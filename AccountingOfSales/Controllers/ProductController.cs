using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountingOfSales.Models;
using PagedList.Mvc;
using PagedList;

namespace AccountingOfSales.Controllers
{
    public class ProductController : Controller
    {
        SalesDbContext db = new SalesDbContext();
        public ActionResult Index(int? page, string name = "")
        {
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            List<Product> products = new List<Product>();

            products = db.Products.ToList();

            if (name != "")
            {
                products = products.Where(n => n.Name.Contains(name)).ToList();
            }

            return View(products.OrderBy(n => n.Name).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            SelectList providers = new SelectList(db.Providers.OrderBy(n => n.Name), "Id", "Name");
            ViewBag.Providers = providers;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, Model, Color, Size, RetailPrice, ProviderId, TypeProductId, ImageId")] Product product)
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}