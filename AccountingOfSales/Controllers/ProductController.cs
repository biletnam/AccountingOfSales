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
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}