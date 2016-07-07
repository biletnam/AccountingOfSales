using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingOfSales.Models
{
    public class User
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        public string Login { get; set; }
        public string FIO { get; set; }
        public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
        public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
        public virtual ICollection<Return> Returns { get; set; } = new List<Return>();
    }
}
