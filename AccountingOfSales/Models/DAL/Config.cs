using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingOfSales.Models.DAL
{
    public sealed class Config
    {
        public static SalesDbContext db
        {
            get
            {
                SalesDbContext _db = null;
                _db = new SalesDbContext();
                return _db;
            }
        }
    }
}
