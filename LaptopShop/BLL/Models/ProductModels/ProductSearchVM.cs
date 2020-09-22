using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaptopShop.BLL.Models.ProductModels
{
    public class ProductSearchVM
    {
        public int Page { get; set; }
        public List<long> Values { get; set; }
    }
}
