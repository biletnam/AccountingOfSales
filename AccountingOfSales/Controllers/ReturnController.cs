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

        public ActionResult Create()
        {
            List<Product> products = db.Products.OrderBy(n => n.Name).ToList();

            ViewBag.Products = new SelectList(products, "Id", "Name");
            ViewBag.TypeReturns = new SelectList(db.TypeReturns.OrderBy(n => n.Name), "Id", "Name");

            if (products.First().Image != null)
                ViewBag.ImageProduct = products.First().Image.Name;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId, ReturnDate, Price, TypeReturnId")] Return newReturn)
        {
            if (ModelState.IsValid)
            {
                User user = UserEntities.GetUserByName(User.Identity.Name);

                newReturn.CreateDate = DateTime.Now;
                if (user != null)
                    newReturn.UserId = user.Id;
                else
                    return HttpNotFound();

                Product product = db.Products.Where(i => i.Id == newReturn.ProductId).FirstOrDefault();
                if (product != null)
                    product.Count = product.Count + 1;
                else
                    return HttpNotFound();

                db.Returns.Add(newReturn);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Create");
        }
        public ActionResult GetImageProduct(int id)
        {
            return PartialView(db.Products.Where(i => i.Id == id).FirstOrDefault());
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}