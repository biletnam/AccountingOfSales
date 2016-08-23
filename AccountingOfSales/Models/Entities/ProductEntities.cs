using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingOfSales.Models.Entities
{
    public class ProductEntities
    {
        static SalesDbContext db = new SalesDbContext();

        public static Product GetProductById(int id)
        {
            return db.Products.Where(i => i.Id == id).FirstOrDefault();
        }
    }
}
