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
        /// <summary>
        /// Дата поступления
        /// </summary>
        public DateTime AdmissionDate { get; set; }
        /// <summary>
        /// Дополнительные расходы
        /// </summary>
        public double AdditionalCosts { get; set; }
        /// <summary>
        /// Оптовая цена
        /// </summary>
        public double TradePrice { get; set; }

        [ScaffoldColumn(false)]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        [ScaffoldColumn(false)]
        public int ProviderId { get; set; }
        public virtual Provider Provider { get; set; }

        [ScaffoldColumn(false)]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
