using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaptopShop.BLL.Models.CaregoryModels
{
    public class CategoryGroupVM
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string UrlSlug { get; set; }
        public string Image { get; set; }

        public int? ParentId { get; set; }

        public List<CategoryGroupVM> Children { get; set; }
    }
}
