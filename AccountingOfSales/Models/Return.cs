using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingOfSales.Models
{
    public class Return
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime CreateDate { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Дата возврата")]
        public DateTime ReturnDate { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Сумма")]
        [RegularExpression(@"[\d]*", ErrorMessage = "Сумма должна содержать только целое число")]
        [Range(0, 1000000000, ErrorMessage = "Недопустимое число")]
        public int Price { get; set; }

        [ScaffoldColumn(false)]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [ScaffoldColumn(false)]
        public int? SalaryId { get; set; }
        public virtual Salary Salary { get; set; }

        [ScaffoldColumn(false)]
        public int TypeReturnId { get; set; }
        public virtual TypeReturn TypeReturn { get; set; }

        [ScaffoldColumn(false)]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
