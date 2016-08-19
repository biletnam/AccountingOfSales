using AccountingOfSales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using AccountingOfSales.Models.Entities;

namespace AccountingOfSales.Controllers
{
    public class SalesController : Controller
    {
        SalesDbContext db = new SalesDbContext();

        public ActionResult Index(int? page, DateTime? filterDateSaleFrom, DateTime? filterDateSaleTo, int? filterUser, int? filterProduct)
        {
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            List<Sale> sales = SalesEntities.GetSales(filterDateSaleFrom, filterDateSaleTo, filterUser, filterProduct);

            //добавляем новый пустой продукт, чтобы был выбор НЕ продукта
            List<Product> products = new List<Product>();
            products.Add(new Product() { Id = 0, Name = "Выберите продукт" });
            products.AddRange(db.Products.OrderBy(n => n.Name));

            List<User> users = new List<User>();
            users.Add(new User() { Id = 0, Login = "Выберите пользователя" });
            users.AddRange(db.Users.OrderBy(n => n.Login));

            ViewBag.Products = new SelectList(products, "Id", "Name");
            ViewBag.Users = new SelectList(users, "Id", "Login");

            return View(sales.OrderByDescending(d => d.SaleDate).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            List<Product> products = db.Products.Where(a => a.Archive == false).OrderBy(n => n.Name).ToList();

            ViewBag.Products = new SelectList(products, "Id", "Name");
            ViewBag.RetailPrice = products.First().RetailPrice;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RetailPrice, Discount, SaleDate, ProductId")] Sale sale)
        {
            return View();
        }

        public ActionResult GetRetailPrice(int id)
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