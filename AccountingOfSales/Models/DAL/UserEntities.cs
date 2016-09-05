using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingOfSales.Models.DAL
{
    public class UserEntities
    {
        static SalesDbContext db = new SalesDbContext();

        public static User GetUserByName(string name)
        {
            return db.Users.Where(n => n.Login == name).FirstOrDefault();
        }
        public static bool IsInRole(string userLogin, string userRole)
        {
            User user = db.Users.Where(l => l.Login == userLogin).FirstOrDefault();
            Role role = db.Roles.Where(n => n.Name == userRole).FirstOrDefault();
            
            if (user.Roles.Contains(role))
                return true;
            else
                return false;
        }
    }
}
