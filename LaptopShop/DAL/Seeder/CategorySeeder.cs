using LaptopShop.DAL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LaptopShop.DAL.Seeder
{
    public class CategorySeeder
    {
        public static void SeedCategories(EFDbContext context)
        {

            string urlSlug = "notebooks";
            #region Parent categories
            if (context.Categories.SingleOrDefault(c => c.UrlSlug == urlSlug) == null)
            {
                context.Categories.Add(new Category
                {
                    Name = "Ноутбуки",
                    ParentId = null,
                    UrlSlug = urlSlug,
                    Image= Path.GetRandomFileName() + ".jpg"
                });
                context.SaveChanges();
            }

            urlSlug = "hard";
            if (context.Categories.SingleOrDefault(c => c.UrlSlug == urlSlug) == null)
            {
                context.Categories.Add(new Category
                {
                    Name = "Комп'ютерні комплектуючі",
                    ParentId = null,
                    UrlSlug = urlSlug,
                    Image = Path.GetRandomFileName() + ".jpg"
                });
                context.SaveChanges();
            }

            #endregion

            #region Child categories

            string chidlUrlSlug = "ssd";
            if (context.Categories.SingleOrDefault(c => c.UrlSlug == chidlUrlSlug) == null)
            {
                var parent = context.Categories.SingleOrDefault(c => c.UrlSlug == urlSlug);
                context.Categories.Add(new Category
                {
                    Name = "SSD диски",
                    ParentId = parent.Id,
                    UrlSlug = chidlUrlSlug,
                    Image = Path.GetRandomFileName() + ".jpg"
                });
                context.SaveChanges();
            }

            chidlUrlSlug = "keybords-mice";
            if (context.Categories.SingleOrDefault(c => c.UrlSlug == chidlUrlSlug) == null)
            {
                var parent = context.Categories.SingleOrDefault(c => c.UrlSlug == urlSlug);
                context.Categories.Add(new Category
                {
                    Name = "Клавіатури та миші",
                    ParentId = parent.Id,
                    UrlSlug = chidlUrlSlug,
                    Image = Path.GetRandomFileName() + ".jpg"
                });
                context.SaveChanges();
            }

            #endregion
        }
    }
}