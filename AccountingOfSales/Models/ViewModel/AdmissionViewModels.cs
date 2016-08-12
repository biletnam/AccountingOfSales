using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AccountingOfSales.Models.ViewModel
{
    public class IndexViewModels
    {
        public DateTime FilterDateAdmissionFrom { get; set; }
        public DateTime FilterDateAdmissionTo { get; set; }
        public string UserName { get; set; }
        PagedList.IPagedList<Admission> Admissions { get; set; }
    }

    public class AdmissionCreateViewModels
    {
        [Display(Name = "Дата создания")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Дата поступления")]
        [Required(ErrorMessage = "Дата поступления не может быть пустым")]
        /// <summary>
        /// Дата поступления
        /// </summary>
        public DateTime AdmissionDate { get; set; }

        [Display(Name = "Дополнительные расходы")]
        [RegularExpression(@"[\d.]*", ErrorMessage = "Поле дополнительных расходов содержит запрещающие символы")]
        [Range(0, 1000000000, ErrorMessage = "В поле дополнительных расходов содержится недопустимое число")]
        /// <summary>
        /// Дополнительные расходы
        /// </summary>
        public double? AdditionalCosts { get; set; }

        [Required(ErrorMessage = "Поле оптовой цены не может быть пустым")]
        [Display(Name = "Оптовая цена")]
        [RegularExpression(@"[\d.]*", ErrorMessage = "Поле оптовой цены содержит запрещающие символы")]
        [Range(0, 1000000000, ErrorMessage = "В поле оптовой цены содержится недопустимое число")]
        /// <summary>
        /// Оптовая цена
        /// </summary>
        public double TradePrice { get; set; }

        [Required(ErrorMessage = "Поле количества не должно быть пустым")]
        [Display(Name = "Количество")]
        [RegularExpression(@"[\d]*", ErrorMessage = "В поле Количество вводить разрешено только целые числа")]
        public int Count { get; set; }

        [ScaffoldColumn(false)]
        public int ProductId { get; set; }

        [ScaffoldColumn(false)]
        public int ProviderId { get; set; }

        [Display(Name = "Сумма")]
        [RegularExpression(@"[\d.]*", ErrorMessage = "Поле Сумма содержит запрещающие символы")]
        [Range(0, 1000000000, ErrorMessage = "В поле Сумма содержится недопустимое число")]
        public double PriceOtherCosts { get; set; }

        [Display(Name = "Комментарий")]
        [StringLength(150, ErrorMessage = "В поле Комментарий количество символов не должно превышать 150")]
        [RegularExpression(@"[\w\d\sА-яёЁ:!?,.()%-]*", ErrorMessage = "В поле Комментарий, текст содержит запрещающие символы")]
        public string CommentOtherCosts { get; set; }
    }
}
