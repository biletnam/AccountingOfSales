using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AccountingOfSales.Models
{
    public class Sale
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Розничная цена")]
        [RegularExpression(@"[\d]*", ErrorMessage = "Розничная цена должна содержать только целое число")]
        [Range(0, 1000000000, ErrorMessage = "Недопустимое число")]
        /// <summary>
        /// Розничная цена
        /// </summary>
        public int RetailPrice { get; set; }

        [Display(Name = "Сумма скидки")]
        [RegularExpression(@"[\d]*", ErrorMessage = "Скидка должна содержать только целое число")]
        [Range(0, 1000000000, ErrorMessage = "Недопустимое число")]
        /// <summary>
        /// Скидка
        /// </summary>
        public int? Discount { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Цена продажи")]
        [RegularExpression(@"[\d]*", ErrorMessage = "Цена продажи должна содержать только целое число")]
        [Range(0, 1000000000, ErrorMessage = "Недопустимое число")]
        /// <summary>
        /// Цена продажи
        /// </summary>
        public int SalePrice { get; set; }

        [Required]
        [Display(Name = "Дата создания")]
        public DateTime CreateDate { get; set; }

        [Required]
        [Display(Name = "Дата продажи")]
        /// <summary>
        /// Дата продажи
        /// </summary>
        public DateTime SaleDate { get; set; }

        [Display(Name = "Начислено")]
        /// <summary>
        /// Показывает, было ли начисление зп, по этой продаже
        /// </summary>
        public bool ACC { get; set; } = false;
        
        [ScaffoldColumn(false)]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [ScaffoldColumn(false)]
        [Remote("CheckCountProduct", "Sales", ErrorMessage = "Остаток равен 0")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
