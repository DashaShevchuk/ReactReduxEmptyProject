using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using LaptopShop.BLL.DTO;
using LaptopShop.BLL.Helpers;
using LaptopShop.BLL.Interfaces;
using LaptopShop.BLL.Models;
using LaptopShop.BLL.Models.ErrorModels;
using LaptopShop.Core.Controller;
using LaptopShop.DAL.Entities;
using LaptopShop.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LaptopShop.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AccountController : WebControllerBase
    {
        private readonly UserManager<DbUser> _userManager;
        private readonly SignInManager<DbUser> _signInManager;
        private readonly IJWTTokenService _jwtTokenService;
        private readonly IRecaptchaService _recaptchaService;
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public AccountController(
            IWebHostEnvironment env,
            IConfiguration configuration,
            UserManager<DbUser> userManager,
            SignInManager<DbUser> signInManager,
            IJWTTokenService jWTTokenService,
            IRecaptchaService recaptchaService,
            ILogger<AccountController> logger
            ) : base(logger)
        {
            this._configuration = configuration;
            this._env = env;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._jwtTokenService = jWTTokenService;
            this._recaptchaService = recaptchaService;
            this._logger = logger;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginUser([FromBody]LoginDTO loginModel)
        {
            // Auto return errors from viewModel and other global errors
            return await HandleRequestAsync(async ()=>
            {
                int countOfAttempts = this.HttpContext.Session.GetInt32("LoginAttemts") ?? 0;
                countOfAttempts++;
                this.HttpContext.Session.SetInt32("LoginAttemts", countOfAttempts);

                this._logger.LogDebug("Start method LoginUser...");
                var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password,false, false);
                if (!result.Succeeded)
                {
                    return BadRequest(new InvalidData 
                    { 
                        Invalid = "Не правильно введені дані", 
                        ShowCaptcha = countOfAttempts > 4 ? true : false
                    });
                }

                var user = await _userManager.FindByEmailAsync(loginModel.Email);
                await _signInManager.SignInAsync(user, isPersistent: false);

                if (countOfAttempts > 4)
                {
                    // TODO: Captcha validation
                    this._recaptchaService.IsValid(loginModel.RecaptchaToken);
                }


                // Return token
                JwtInfo jwtInfo = new JwtInfo()
                {
                    Token = _jwtTokenService.CreateToken(user),
                    RefreshToken= _jwtTokenService.CreateRefreshToken(user)
                };

                this.HttpContext.Session.SetInt32("LoginAttemts", 0);
                this._logger.LogDebug("End method LoginUser...");

                return Ok(jwtInfo);
            });
        }

        [HttpPost]
        [Route("refreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody]TokensDTO tokens)
        {
            return await HandleRequestAsync(async () =>
            {
                return Ok(await _jwtTokenService.RefreshAuthToken(tokens.Token, tokens.RefreshToken));
            });
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterDTO model)
        {
            // Auto return errors from viewModel and other global errors
            return await HandleRequestAsync(async () =>
            {
                if (!CaptchaHelper.VerifyAndExpireSolution(this.HttpContext, 
                    model.CaptchaKey,
                    model.CaptchaText))
                {
                    var invalid = new Dictionary<string, string>();
                    invalid.Add("captchaText", "Помилка вводу зображення на фото");
                    return BadRequest(invalid);
                }
                var user = _userManager.FindByEmailAsync(model.Email).Result; 
                if (user != null)
                {
                    var invalid = new Dictionary<string, string>();
                    invalid.Add("email", "Користувач з даною електронною поштою уже зареєстрований");
                    return BadRequest(invalid);
                }

                string imageName = Path.GetRandomFileName() + ".jpg";

                try
                {
                    this._logger.LogDebug("Start register method RegisterUser...");
                    string pathSaveImages = InitStaticFiles
                           .CreateImageByFileName(_env, _configuration,
                                new string[] { "ImagesPath", "ImagesPathUser" },
                                imageName,
                                model.Photo);
                    this._logger.LogDebug("Save image complete method RegisterUser...");
                    if (pathSaveImages != null)
                    {
                        var profile = new UserProfile()
                        {
                            RegistrationDate = DateTime.Now,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Photo=imageName,

                        };
                        user = new DbUser()
                        {
                            UserName = model.Email,
                            Email = model.Email,
                            PhoneNumber = model.Phone,
                            UserProfile = profile
                        };
                        var result = _userManager.CreateAsync(user, model.Password).Result;
                        if (!result.Succeeded)
                        {
                            var errors = CustomValidator.GetErrorsByIdentityResult(result);
                            return BadRequest(errors);
                        }
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        // Return token
                        JwtInfo jwtInfo = new JwtInfo()
                        {
                            Token = _jwtTokenService.CreateToken(user),
                            RefreshToken = _jwtTokenService.CreateRefreshToken(user)
                        };

                        this._logger.LogDebug("End method RegisterUser...");

                        return Ok(jwtInfo);
                    }
                    else
                        throw new Exception("Помила додавання фото в БД");
                }
                catch(Exception ex)
                {
                    InitStaticFiles.DeleteImageByFileName(_env, _configuration,
                                new string[] { "ImagesPath", "ImagesPathUser" },
                                imageName);
                    var errors = new Dictionary<string, string>();
                    errors.Add("invalid", ex.Message);
                    return BadRequest(errors);
                }

            });
        }
    }
}