using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LaptopShop.BLL.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Поле не може бути пустим!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле не може бути пустим!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Поле не може бути пустим!")]
        [EmailAddress(ErrorMessage = "Не правильний формат електронної пошти!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле не може бути пустим!")]
        [RegularExpression(@"^(?=\+?([0-9]{2})\s\(?([0-9]{3})\)\s?([0-9]{3})\s?([0-9]{2})\s?([0-9]{2})).{19}$", ErrorMessage = "Не правильний формат номера телефону!")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Поле не може бути пустим!")]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).{6,24}$", ErrorMessage = "Пароль повинен мати мінімум 6 символів, нижній і верхній регістр, та цифри!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Поле не може бути пустим!")]
        [Compare("Password", ErrorMessage = "Паролі не співпадають!")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Поле не може бути пустим!")]
        public string Photo { get; set; }

        [Required(ErrorMessage = "Поле не може бути пустим!")]
        public string CaptchaText { get; set; }

        public string CaptchaKey { get; set; }
    }
}
