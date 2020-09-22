using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaptopShop.Core.Controller;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LaptopShop.Controllers
{
    //[Produces("application/json")]
    //[Route("api/[controller]")]
    //public class TestController : ControllerBase
    //{
    //    [HttpGet]
    //    [Authorize]
    //    public IActionResult Get()
    //    {
    //        return Ok("Test text");
    //    }
    //}

    [Produces("application/json")]
    [Route("api/[controller]")]
    public class TestController : WebControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(
            ILogger<TestController> logger
            ) : base(logger)
        {
            
            this._logger = logger;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            // Auto return errors from viewModel and other global errors
            return await HandleRequestAsync(async () =>
            {
               
                return Ok("Test text");
            });
        }


        //public IActionResult Get()
        //{
        //    return Ok("Test text");
        //}
    }
}