using AccountingOfSales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using AccountingOfSales.Models.Entities;
using System.Web.UI;

namespace AccountingOfSales.Controllers
{
    [Authorize]
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

            return View(sales.OrderByDescending(d => d.SaleDate).ThenByDescending(d => d.CreateDate).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create(bool? createSale)
        {
            List<Product> products = db.Products.Where(a => a.Archive == false).OrderBy(n => n.Name).ToList();

            ViewBag.Products = new SelectList(products, "Id", "Name");
            ViewBag.RetailPrice = products.First().RetailPrice;

            if(products.First().Image != null)
                ViewBag.ImageProduct = products.First().Image.Name;

            ViewBag.CreateSale = createSale;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RetailPrice, Discount, SaleDate, ProductId")] Sale sale)
        {
            if (ModelState.IsValid)
            {
                User user = UserEntities.GetUserByName(User.Identity.Name);

                sale.CreateDate = DateTime.Now;
                if (sale.Discount != null)
                    sale.SalePrice = sale.RetailPrice - (int)sale.Discount;
                else
                    sale.SalePrice = sale.RetailPrice;
                if (user != null)
                    sale.UserId = user.Id;
                else
                    return HttpNotFound();

                db.Sales.Add(sale);

                Product product = db.Products.Where(i => i.Id == sale.ProductId).FirstOrDefault();
                if (product != null)
                    product.Count = product.Count - 1;
                else
                    return HttpNotFound();

                db.SaveChanges();
                return RedirectToAction("Create", new { createSale = true });
            }

            return RedirectToAction("Create");
        }

        public ActionResult GetRetailPrice(int id)
        {
            return PartialView(db.Products.Where(i => i.Id == id).FirstOrDefault());
        }
        public ActionResult GetImageProduct(int id)
        {
            return PartialView(db.Products.Where(i => i.Id == id).FirstOrDefault());
        }

        public JsonResult CheckCountProduct(int ProductId)
        {
            Product product = db.Products.Where(i => i.Id == ProductId).FirstOrDefault();
            if(product.Count == 0)
                return Json(false, JsonRequestBehavior.AllowGet);
            else
                return Json(true, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}