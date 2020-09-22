using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaptopShop.BLL.Models
{
    public class JwtInfo
    {
        public string Token { get; set; }

        public string RefreshToken { get; set; }
    }
}
