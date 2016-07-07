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
        public DateTime CreateDate { get; set; }
        public DateTime ReturnDate { get; set; }

        [ScaffoldColumn(false)]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [ScaffoldColumn(false)]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
