using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LaptopShop.BLL.Interfaces
{
    public interface IRecaptchaService
    {
        /// <summary>
        /// Check is recaptchaToken si valid
        /// </summary>
        /// <param name="recaptchaToken">Recaptcha token</param>
        /// <returns></returns>
        public bool IsValid(string recaptchaToken);
    }
}
