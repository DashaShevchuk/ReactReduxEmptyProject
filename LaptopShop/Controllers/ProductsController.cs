using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LaptopShop.BLL.Models;
using LaptopShop.BLL.Models.ProductModels;
using LaptopShop.Core.Controller;
using LaptopShop.DAL.Entities;
using LaptopShop.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LaptopShop.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProductsController : WebControllerBase
    {
        private readonly EFDbContext _context;
        private readonly ILogger<CategoryController> _logger;

        public ProductsController(EFDbContext context,
            ILogger<CategoryController> logger)
            : base(logger)
        {
            this._context = context;
            this._logger = logger;
        }
        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody]ProductSearchVM search)
        {
            // Auto return errors from viewModel and other global errors
            return await HandleRequestAsync(async () =>
            {
                var z = search;
                Thread.Sleep(2000);
                long[] values = { };
                var filtersList = GetListFilters(_context);
                long[] filterValueSearchList = values; //масив ID вибраних фільтрів
                var query = _context
                    .Products
                    .AsQueryable();
                foreach (var fName in filtersList)
                {
                    int countFilter = 0; //Кількість співпадінь у даній групі фільтрів
                    var predicate = PredicateBuilder.False<Product>();
                    foreach (var fValue in fName.Children)
                    {
                        for (int i = 0; i < filterValueSearchList.Length; i++)
                        {
                            var idV = fValue.Id;
                            if (filterValueSearchList[i] == idV)
                            {
                                predicate = predicate
                                    .Or(p => p.Filtres
                                        .Any(f => f.FilterValueId == idV));
                                countFilter++;
                            }
                        }
                    }
                    if (countFilter != 0)
                        query = query.Where(predicate);
                }
                int count = query.Count();

                var res = query
                    .Select(p=>new
                    {
                        Id=p.Id,
                        Name=p.Name,
                        Price=p.Price,
                        Filters = p.Filtres
                            .Select(f=>new
                            {
                                Filter = f.FilterNameOf.Name,
                                ValueId = f.FilterValueId,
                                Value=f.FilterValueOf.Name
                            })

                    })
                    .ToList();
               
                return Ok(res);
            });
        }

        private List<FNameViewModel> GetListFilters(EFDbContext context)
        {
            var queryName = from f in context.FilterNames.AsQueryable()
                            select f;
            var queryGroup = from g in context.FilterNameGroups.AsQueryable()
                             select g;

            //Отримуємо загальну множину значень
            var query = from u in queryName
                        join g in queryGroup on u.Id equals g.FilterNameId into ua
                        from aEmp in ua.DefaultIfEmpty()
                        select new
                        {
                            FNameId = u.Id,
                            FName = u.Name,
                            FValueId = aEmp != null ? aEmp.FilterValueId : 0,
                            FValue = aEmp != null ? aEmp.FilterValueOf.Name : null,
                        };

            //Групуємо по іменам і сортуємо по спаданню імен
            //var groupNames = (from f in query
            //                  group f by new
            //                  {
            //                      Id = f.FNameId,
            //                      Name = f.FName
            //                  } into g
            //                  //orderby g.Key.Name
            //                  select g).OrderByDescending(g => g.Key.Name).AsEnumerable();

            var groupNames = query
                        .AsEnumerable()
                        .GroupBy(f => new { Id = f.FNameId, Name = f.FName })
                        .Select(g => g)
                        .OrderByDescending(p => p.Key.Name);

            //По групах отримуємо
            var result = from fName in groupNames
                         select
                         new FNameViewModel
                         {
                             Id = fName.Key.Id,
                             Name = fName.Key.Name,
                             Children = (from v in fName
                                         group v by new FValueViewModel
                                         {
                                             Id = v.FValueId,
                                             Value = v.FValue
                                         } into g
                                         select g.Key)
                                         .OrderBy(l => l.Value).ToList()
                         };

            return result.ToList();
        }
    }
}