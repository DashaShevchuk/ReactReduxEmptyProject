using LaptopShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaptopShop.DAL.Seeder
{
    public class FilterSeeder
    {
        public static void SeedFilters(EFDbContext context)
        {
            #region tblFilterNames - Назви фільтрів
            string urlSlug = "notebooks";
            string[] filterNames = { "Виробник", "Діагональ екрана" };
            var c = context.Categories.SingleOrDefault(c => c.UrlSlug == urlSlug);
            
            foreach (var type in filterNames)
            {
                if (context.FilterNames.SingleOrDefault(f => f.Name == type) == null)
                {
                    context.FilterNames.Add(
                        new FilterName
                        {
                            Name = type,
                            CategoryId=c.Id
                        });
                    context.SaveChanges();
                }
            }
            #endregion

            #region tblFilterValues - Значення фільтрів
            List<string[]> filterValues = new List<string[]> {
                new string [] { "HP", "Dell", "Apple" },
                new string [] { "13\"", "14\"", "15\"-15.6\"", "16\"-17\"" }
                
            };
            
            foreach (var items in filterValues)
            {
                foreach (var value in items)
                {
                    if (context.FilterValues
                        .SingleOrDefault(f => f.Name == value) == null)
                    {
                        context.FilterValues.Add(
                            new FilterValue
                            {
                                Name = value
                            });
                        context.SaveChanges();
                    }
                }
            }
            #endregion

            #region tblFilterNameGroups - Групування по групах фільтрів
            for (int i = 0; i < filterNames.Length; i++)
            {
                foreach (var value in filterValues[i])
                {
                    var nId = context.FilterNames
                        .SingleOrDefault(f => f.Name == filterNames[i]).Id;
                    var vId = context.FilterValues
                        .SingleOrDefault(f => f.Name == value).Id;
                    if (context.FilterNameGroups
                        .SingleOrDefault(f => f.FilterValueId == vId &&
                        f.FilterNameId == nId) == null)
                    {
                        context.FilterNameGroups.Add(
                            new FilterNameGroup
                            {
                                FilterNameId = nId,
                                FilterValueId = vId
                            });
                        context.SaveChanges();
                    }
                }
            }
            #endregion

            #region tblProducts - Продукти
            List<string> prods = new List<string>{
                //Path.GetRandomFileName()
             "154muv2f", "154m2fas"
            };
            if (context.Products.SingleOrDefault(f => f.UniqueName == prods[0]) == null)
                context.Products.Add(
                        new Product
                        {
                            UniqueName = prods[0],
                            Price = 32599,
                            CategoryId = c.Id,
                            Name = "Ноутбук HP EliteBook 850 G6 (6XD79EA) Silver"
                        });

            if (context.Products.SingleOrDefault(f => f.UniqueName == prods[1]) == null)
                context.Products.Add(
                  new Product
                  {
                      UniqueName = prods[1],
                      Price = 93999,
                      CategoryId = c.Id,
                      Name = "Dell XPS 15 7590 (X7590UTI932S10ND1650W-9S) Silver"
                  });
            context.SaveChanges();

            #endregion

            #region tblFilters -Фільтри
            Filter[] filters =
            {
                new Filter { FilterNameId = 1, FilterValueId=1, ProductId=1 },
                new Filter { FilterNameId = 2, FilterValueId=5, ProductId=1 },
            
                new Filter { FilterNameId = 1, FilterValueId=2, ProductId=2 },
                new Filter { FilterNameId = 2, FilterValueId=6, ProductId=2 }
            };
            foreach (var item in filters)
            {
                var f = context.Filters.SingleOrDefault(p => p == item);
                if (f == null)
                {
                    context.Filters.Add(new Filter { FilterNameId = item.FilterNameId, FilterValueId = item.FilterValueId, ProductId = item.ProductId });
                    context.SaveChanges();
                }
            }
            #endregion
        }

    }
}
