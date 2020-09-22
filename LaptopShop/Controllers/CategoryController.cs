using LaptopShop.BLL.Helpers;
using LaptopShop.BLL.Models.CaregoryModels;
using LaptopShop.Core.Controller;
using LaptopShop.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaptopShop.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CategoryController : WebControllerBase
    {
        private readonly EFDbContext _context;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(EFDbContext context,
            ILogger<CategoryController> logger) 
            : base(logger)
        {
            this._context = context;
            this._logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // Auto return errors from viewModel and other global errors
            return await HandleRequestAsync(async() =>
            {
                var categories = _context.Categories.Select(ic => new CategoryGroupVM
                {
                    Id = ic.Id,
                    Name = ic.Name,
                    UrlSlug = ic.UrlSlug,
                    ParentId = ic.ParentId
                }).ToList();
                var tree = categories.BuildTree(); //CategoryHelper.BuildTree(listCategory);
                return Ok(tree);
            });
        }
    }
}
