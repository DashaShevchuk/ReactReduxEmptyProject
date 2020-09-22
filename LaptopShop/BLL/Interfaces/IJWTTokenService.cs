using LaptopShop.BLL.DTO;
using LaptopShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaptopShop.BLL.Interfaces
{
    public interface IJWTTokenService
    {
        string CreateToken(DbUser user);
        string CreateRefreshToken(DbUser user);
        Task<TokensDTO> RefreshAuthToken(string oldAuthToken, string refreshToken);
    }
}
