using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaptopShop.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LaptopShop.Core.Controller
{
    public abstract class WebControllerBase: Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly ILogger<WebControllerBase> _logger;

        protected WebControllerBase(
            ILogger<WebControllerBase> logger)
        {
            _logger = logger;
        }

        protected IActionResult HandleRequest(Func<IActionResult> request)
        {
            if (ModelIsValid)
            {
                try
                {
                    return request();
                }
                catch (Exception ex)
                {
                    this._logger.LogError(ex, $"Internal Server Error. Description: {ex.Message}");
                    return BadRequest(new {global = ex.Message});
                }
            }

            var errors = CustomValidator.GetErrorsByModel(ModelState);
            return BadRequest(errors);
        }

        protected async Task<IActionResult> HandleRequestAsync(Func<Task<IActionResult>> request)
        {
            if (ModelIsValid)
            {
                try
                {
                    return await request();
                }
                catch (Exception ex)
                {
                    this._logger.LogError(ex, $"Internal Server Error. Description: {ex.Message}");
                    return BadRequest(new { global = ex.Message });
                }
            }

            var errors = CustomValidator.GetErrorsByModel(ModelState);
            return BadRequest(errors);
        }

        // Sometimes for FilterInfoBase ModelState.IsValid == false, but there are no errors.
        private bool ModelIsValid => ModelState.IsValid || ModelState.ErrorCount == 0;
    }
}
