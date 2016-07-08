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
        /// <summary>
        /// Розничная цена
        /// </summary>
        public double RetailPrice { get; set; }
        /// <summary>
        /// Скидка
        /// </summary>
        public double Discount { get; set; }
        /// <summary>
        /// Цена продажи
        /// </summary>
        public double SalePrice { get; set; }
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// Дата продажи
        /// </summary>
        public DateTime SaleDate { get; set; }

        [ScaffoldColumn(false)]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
