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

        [Display(Name = "Дата создания")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Дата поступления")]
        /// <summary>
        /// Дата поступления
        /// </summary>
        public DateTime AdmissionDate { get; set; }

        [Display(Name = "Дополнительные расходы")]
        [RegularExpression(@"[\d.]*", ErrorMessage = "Текст содержит запрещающие символы")]
        [Range(0, 1000000000, ErrorMessage = "Недопустимое число")]
        /// <summary>
        /// Дополнительные расходы
        /// </summary>
        public double? AdditionalCosts { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Оптовая цена")]
        [RegularExpression(@"[\d.]*", ErrorMessage = "Текст содержит запрещающие символы")]
        [Range(0, 1000000000, ErrorMessage = "Недопустимое число")]
        /// <summary>
        /// Оптовая цена
        /// </summary>
        public double TradePrice { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Количество")]
        [RegularExpression(@"[\d]*", ErrorMessage = "Вводить разрешено только целые числа")]
        public int Count { get; set; }

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
