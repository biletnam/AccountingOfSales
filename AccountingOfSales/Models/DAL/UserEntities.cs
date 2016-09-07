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
            Role role = Config.db.Users.Where(l => l.Login == userLogin).SelectMany(r => r.Roles).Where(r => r.Name == userRole).FirstOrDefault();

            if (role != null)
                return true;
            else
                return false;
        }
    }
}
