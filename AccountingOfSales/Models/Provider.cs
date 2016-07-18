using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AccountingOfSales.Models
{
    public class Provider
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Наименование")]
        [StringLength(150, ErrorMessage = "Количество символов не должно превышать 150")]
        [RegularExpression(@"[\w\d\sА-яёЁ:!?,.()%-]*", ErrorMessage = "Текст содержит запрещающие символы")]
        [Remote("CheckName", "Provider", ErrorMessage = "Поставщик с таким наименованием уже имеется", AdditionalFields = "Id")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Город")]
        [StringLength(150, ErrorMessage = "Количество символов не должно превышать 150")]
        [RegularExpression(@"[\w\d\sА-яёЁ:!?,.()%-]*", ErrorMessage = "Текст содержит запрещающие символы")]
        public string City { get; set; }
        public bool Archive { get; set; } = false;
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<Admission> Admission { get; set; } = new List<Admission>();
    }
}
