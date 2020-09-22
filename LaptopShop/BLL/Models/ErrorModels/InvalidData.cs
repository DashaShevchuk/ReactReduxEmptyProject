using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaptopShop.BLL.Models.ErrorModels
{
    public class InvalidData
    {
        public string Invalid { get; set; }

        public bool ShowCaptcha { get; set; } = false;
    }
}
