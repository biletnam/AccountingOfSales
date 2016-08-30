using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingOfSales.Models
{
    public class Salary
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Display(Name = "Дата С")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Дата До")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Сумма")]
        public int Price { get; set; }

        [ScaffoldColumn(false)]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
