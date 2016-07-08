using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingOfSales.Models
{
    public class Product
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public DateTime CreateDate { get; set; }
        public int Count { get; set; }
        /// <summary>
        /// Розничная цена
        /// </summary>
        public double RetailPrice { get; set; }

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
    }
}
