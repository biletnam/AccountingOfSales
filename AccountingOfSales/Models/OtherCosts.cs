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

        [Display(Name = "Дата расхода")]
        [Required(ErrorMessage = "\"Дата расхода\" не может быть пустой")]
        public DateTime CostsDate { get; set; }

        [Display(Name = "Сумма")]
        [Required(ErrorMessage = "\"Сумма\" не может быть пустой")]
        [RegularExpression(@"[\d.]*", ErrorMessage = "\"Сумма\" содержит запрещающие символы")]
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
