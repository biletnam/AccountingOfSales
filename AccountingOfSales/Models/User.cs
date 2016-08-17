using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingOfSales.Models
{
    public class User
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Логин")]
        [RegularExpression(@"[\w\d]*", ErrorMessage = "Логин должен содержать только латинские алфавитно-цифровые символы.")]
        [StringLength(150, ErrorMessage = "Количество символов не должно превышать 150")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [StringLength(150, ErrorMessage = "Количество символов не должно превышать 150")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "ФИО")]
        [RegularExpression(@"[\w\dА-яёЁ]*", ErrorMessage = "В ФИО должны содержаться только алфавитно-цифровые символы.")]
        [StringLength(150, ErrorMessage = "Количество символов не должно превышать 150")]
        public string FIO { get; set; }
        public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
        public virtual ICollection<Return> Returns { get; set; } = new List<Return>();
        public virtual ICollection<Salary> Salaries { get; set; } = new List<Salary>();
    }
}
