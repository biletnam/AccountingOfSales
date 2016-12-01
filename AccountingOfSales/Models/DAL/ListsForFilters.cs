using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AccountingOfSales.Models.DAL
{
    public class ListsForFilters
    {
        static List<SelectListItem> acc;
        static List<User> users;
        static List<Product> products;
        static List<TypeReturn> typesReturn;
        static List<Provider> providers;

        public static List<SelectListItem> ACC {
            get
            {
                acc = new List<SelectListItem>();
                acc.Add(new SelectListItem() { Text = "Начислено", Value = "-1" });
                acc.Add(new SelectListItem() { Text = "Да", Value = "1" });
                acc.Add(new SelectListItem() { Text = "Нет", Value = "0" });
                return acc;
            }
        }
        public static List<User> Users
        {
            get
            {
                users = new List<User>();
                users.Add(new User() { Id = 0, Login = "Выберите пользователя" });
                users.AddRange(Config.db.Users.OrderBy(n => n.Login));
                return users;
            }
        }
        public static List<Product> Products
        {
            get
            {
                products = new List<Product>();
                products.Add(new Product() { Id = 0, Name = "Выберите продукт" });
                products.AddRange(Config.db.Products.OrderBy(n => n.Name));
                return products;
            }
        }
        public static List<TypeReturn> TypeReturns
        {
            get
            {
                typesReturn = new List<TypeReturn>();
                typesReturn.Add(new TypeReturn() { Id = 0, Name = "Выберите тип возврата" });
                typesReturn.AddRange(Config.db.TypeReturns.OrderBy(n => n.Name));
                return typesReturn;
            }
        }
        public static List<Provider> Providers
        {
            get
            {
                providers = new List<Provider>();
                providers.Add(new Provider() { Id = 0, Name = "Выберите поставщика" });
                providers.AddRange(Config.db.Providers.OrderBy(n => n.Name));
                return providers;
            }
        }
    }
}
