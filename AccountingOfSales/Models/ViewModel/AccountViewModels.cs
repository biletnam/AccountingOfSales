using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AccountingOfSales.Models.ViewModel
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Логин")]
        [RegularExpression(@"[\w\d]*", ErrorMessage = "Логин должен содержать только латинские алфавитно-цифровые символы.")]
        public string Login { get; set; }

        [Display(Name = "Запомнить меня")]
        public bool RememberMe { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [StringLength(10, ErrorMessage = "Пароль должен содержать не менее 1 и не более 10 символов.", MinimumLength = 1)]
        public string Password { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Логин")]
        [RegularExpression(@"[\w\d]*", ErrorMessage = "Логин должен содержать только латинские алфавитно-цифровые символы.")]
        [Remote("CheckLogin", "Account", ErrorMessage = "Пользователь с таким логином уже имеется", AdditionalFields = "Id")]
        public string Login { get; set; }

        [Required]
        [Display(Name = "ФИО")]
        [RegularExpression(@"[\w\dА-яёЁ]*", ErrorMessage = "В ФИО должны содержаться только алфавитно-цифровые символы.")]
        [StringLength(150, ErrorMessage = "Количество символов не должно превышать 150")]
        public string FIO { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [StringLength(10, ErrorMessage = "Пароль должен содержать не менее 1 и не более 10 символов.", MinimumLength = 1)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}
