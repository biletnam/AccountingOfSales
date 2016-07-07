using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingOfSales.Models
{
    public class Admission
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime AdmissionDate { get; set; } //дата поступления
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

        [ScaffoldColumn(false)]
        public int ProviderId { get; set; }
        public virtual Provider Provider { get; set; }
        public double AdditionalCosts { get; set; } //дополнительные расходы

        [ScaffoldColumn(false)]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
