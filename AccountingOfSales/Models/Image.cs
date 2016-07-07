using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingOfSales.Models
{
    public class Image
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public string Extension { get; set; }
        public string Name
        {
            get
            {
                return $"{Id}.{Extension}";
            }
        }
    }
}
