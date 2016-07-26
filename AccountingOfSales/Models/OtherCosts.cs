using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingOfSales.Models
{
    public class OtherCosts
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime CostsDate { get; set; }
        public double Price { get; set; }
        public string Comment { get; set; }

        [ScaffoldColumn(false)]
        public int? AdmissionId { get; set; }
        public virtual Admission Admission { get; set; }
    }
}
