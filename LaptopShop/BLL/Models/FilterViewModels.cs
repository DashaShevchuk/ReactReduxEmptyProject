using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaptopShop.BLL.Models
{
    public class FValueViewModel
    {
        public long Id { get; set; }
        public string Value { get; set; }
        public bool IsChecked { get; set; } = false;
    }
    public class FNameGetViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Children { get; set; }
    }

    public class FNameViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsCollapsed { get; set; } = true;
        public List<FValueViewModel> Children { get; set; }
    }

    public class FilterVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
