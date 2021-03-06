﻿using LaptopShop.BLL.DTO;
using LaptopShop.BLL.Interfaces;
using LaptopShop.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LaptopShop.BLL.Services
{
    public class JWTTokenService : IJWTTokenService
    {
        private readonly EFDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<DbUser> _userManager;
        public JWTTokenService(EFDbContext context,
        IConfiguration configuration,
            UserManager<DbUser> userManager)
        {
            _context = context;
            _configuration = configuration;
            _userManager = userManager;
        }

        public string CreateRefreshToken(DbUser user)
        {
            var _refreshToken = _context.RefreshTokens
               .SingleOrDefault(m => m.Id == user.Id);

            if (_refreshToken == null)
            {
                RefreshToken t = new RefreshToken
                {
                    Id = user.Id,
                    Token = Guid.NewGuid().ToString()
                };
                _context.RefreshTokens.Add(t);
                _context.SaveChanges();
                _refreshToken = t;
            }
            else
            {
                _refreshToken.Token = Guid.NewGuid().ToString();
                _context.RefreshTokens.Update(_refreshToken);
                _context.SaveChanges();
            }

            return _refreshToken.Token;
        }

        public string CreateToken(DbUser user)
        {
            var roles = _userManager.GetRolesAsync(user).Result;
            var claims = new List<Claim>()
            {
                //new Claim(JwtRegisteredClaimNames.Sub, user.Id)
                new Claim("id", user.Id.ToString()),
                new Claim("name", user.UserName)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim("roles", role));
            }

            string jwtTokenSecretKey = this._configuration.GetValue<string>("SecretPhrase");

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenSecretKey));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                claims: claims,
                expires: DateTime.Now.AddMinutes(1));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public async Task<TokensDTO> RefreshAuthToken(string oldAuthToken, string refreshToken)
        {
            var principal = this.GetPrincipalFromExpiredToken(oldAuthToken);
            var username = principal.FindFirstValue("name");
            var user = await _userManager.Users.FirstOrDefaultAsync(t => t.UserName == username);
            if (user == null) throw new Exception("Tokens are invalid");

            var userRefreshToken = 
                 await _context.RefreshTokens
                .Include(t=>t.User)
                .SingleOrDefaultAsync(t => t.User.Id == user.Id && t.Token == refreshToken);

            if (userRefreshToken == null) throw new Exception("Tokens are invalid");

            TokensDTO model = new TokensDTO()
            {
                Token = CreateToken(user),
                RefreshToken = CreateRefreshToken(user)
            };

            return model;
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            string jwtTokenSecretKey = this._configuration.GetValue<string>("SecretPhrase");

            var tokenValidationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtTokenSecretKey)),
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }

}
