using AccountingOfSales.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountingOfSales.Models.Entities;

namespace AccountingOfSales.Controllers
{
    [Authorize]
    public class ReturnController : Controller
    {
        SalesDbContext db = new SalesDbContext();

        public ActionResult Index(int? page, DateTime? filterDateReturnFrom, DateTime? filterDateReturnTo, int? filterUser, int? filterProduct, int? filterTypesReturn)
        {
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            List<Return> returns = ReturnEntities.GetReturns(filterDateReturnFrom, filterDateReturnTo, filterUser, filterProduct, filterTypesReturn);

            //добавляем новый пустой продукт, чтобы был выбор НЕ продукта
            List<Product> products = new List<Product>();
            products.Add(new Product() { Id = 0, Name = "Выберите продукт" });
            products.AddRange(db.Products.OrderBy(n => n.Name));

            List<User> users = new List<User>();
            users.Add(new User() { Id = 0, Login = "Выберите пользователя" });
            users.AddRange(db.Users.OrderBy(n => n.Login));

            List<TypeReturn> typesReturn = new List<TypeReturn>();
            typesReturn.Add(new TypeReturn() { Id=0, Name = "Выберите тип возврата" });
            typesReturn.AddRange(db.TypeReturns.OrderBy(n => n.Name));

            ViewBag.Products = new SelectList(products, "Id", "Name");
            ViewBag.Users = new SelectList(users, "Id", "Login");
            ViewBag.TypesReturn = new SelectList(typesReturn, "Id", "Name");

            return View(returns.OrderByDescending(d => d.ReturnDate).ThenByDescending(d => d.CreateDate).ToPagedList(pageNumber, pageSize));
        }
    }
}