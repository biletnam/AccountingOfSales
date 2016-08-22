using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingOfSales.Models.Entities
{
    public class UserEntities
    {
        static SalesDbContext db = new SalesDbContext();

        public static User GetUserByName(string name)
        {
            return db.Users.Where(n => n.Login == name).FirstOrDefault();
        }
    }
}
