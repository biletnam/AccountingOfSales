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

        [Required]
        public DateTime CreateDate { get; set; }

        [Required(ErrorMessage = "Поле \"Дата расхода\" не может быть пустым")]
        public DateTime CostsDate { get; set; }

        [Display(Name = "Сумма")]
        [Required(ErrorMessage = "Поле \"Сумма\" не может быть пустым")]
        [RegularExpression(@"[\d.]*", ErrorMessage = "Сумма содержит запрещающие символы")]
        [Range(0, 1000000000, ErrorMessage = "Недопустимое число")]
        public double Price { get; set; }

        [Display(Name = "Комментарий")]
        [StringLength(150, ErrorMessage = "Количество символов не должно превышать 150")]
        [RegularExpression(@"[\w\d\sА-яёЁ:!?,.()%-]*", ErrorMessage = "Текст содержит запрещающие символы")]
        public string Comment { get; set; }
        /// <summary>
        /// Показывает, что расход был создан при создании поступления
        /// </summary>
        public bool Admission { get; set; }
    }
}
