using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingOfSales.Models.DAL
{
    public class ProductEntities
    {
        public static Product GetProductById(int id)
        {
            return Config.db.Products.Where(i => i.Id == id).FirstOrDefault();
        }
    }
}
