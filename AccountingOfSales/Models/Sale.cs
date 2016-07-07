using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingOfSales.Models
{
    public class Sale
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public double RetailPrice { get; set; } //розничная цена
        public double Discount { get; set; }    //скидка
        public double SalePrice { get; set; }   //цена продажи
        public DateTime CreateDate { get; set; }
        public DateTime SaleDate { get; set; }  //дата продажи

        [ScaffoldColumn(false)]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
