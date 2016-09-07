using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingOfSales.Models.DAL
{
    public class UserEntities
    {
        public static User GetUserByName(string name)
        {
            return Config.db.Users.Where(n => n.Login == name).FirstOrDefault();
        }
        public static bool IsInRole(string userLogin, string userRole)
        {
            User user = Config.db.Users.Where(l => l.Login == userLogin).FirstOrDefault();
            Role role = Config.db.Roles.Where(n => n.Name == userRole).FirstOrDefault();
            
            if (user.Roles.Contains(role))
                return true;
            else
                return false;
        }
    }
}
