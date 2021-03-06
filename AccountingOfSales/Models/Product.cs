﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AccountingOfSales.Models
{
    public class Product
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Наименование")]
        [StringLength(150, ErrorMessage = "Количество символов не должно превышать 150")]
        [RegularExpression(@"[\w\d\sА-яёЁ:!?,.()%-]*", ErrorMessage = "Текст содержит запрещающие символы")]
        [Remote("CheckName", "Product", ErrorMessage = "Товар с таким наименованием уже имеется", AdditionalFields = "Id")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Модель")]
        [StringLength(150, ErrorMessage = "Количество символов не должно превышать 150")]
        [RegularExpression(@"[\w\d\sА-яёЁ:!?,.()%-]*", ErrorMessage = "Текст содержит запрещающие символы")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Цвет")]
        [StringLength(50, ErrorMessage = "Количество символов не должно превышать 50")]
        [RegularExpression(@"[\w\d\sА-яёЁ:!?,.()%-]*", ErrorMessage = "Текст содержит запрещающие символы")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Размер")]
        [StringLength(50, ErrorMessage = "Количество символов не должно превышать 50")]
        [RegularExpression(@"[\w\d\sА-яёЁ:!?,.()%-]*", ErrorMessage = "Текст содержит запрещающие символы")]
        public string Size { get; set; }

        [Display(Name = "Дата создания")]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Дата изменения")]
        public DateTime? EditDate { get; set; }

        [Display(Name = "Количество")]
        [RegularExpression(@"[\d]*", ErrorMessage = "Количество должно содержать только целое число")]
        public int Count { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Display(Name = "Розничная цена")]
        [RegularExpression(@"[\d]*", ErrorMessage = "Розничная цена должна содержать только целое число")]
        [Range(0, 1000000000, ErrorMessage = "Недопустимое число")]
        /// <summary>
        /// Розничная цена
        /// </summary>
        public int RetailPrice { get; set; }
        public bool Archive { get; set; } = false;
        public string DisplayNameSizeColor
        {
            get { return Name + ", " + Size + ", " + Color; }
        }

        [ScaffoldColumn(false)]
        public int? ProviderId { get; set; }
        public virtual Provider Provider { get; set; }

        [ScaffoldColumn(false)]
        public int? TypeProductId { get; set; }
        public virtual TypeProduct TypeProduct { get; set; }

        [ScaffoldColumn(false)]
        public int? ImageId { get; set; }
        public virtual Image Image { get; set; }
        public virtual ICollection<Admission> Admissions { get; set; } = new List<Admission>();
        public virtual ICollection<Return> Returns { get; set; } = new List<Return>();
        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
