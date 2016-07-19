using AccountingOfSales.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountingOfSales.Controllers
{
    public class TypeProductController : Controller
    {
        SalesDbContext db = new SalesDbContext();

        // GET: TypeProduct
        public ActionResult Index(int? page, string filterName = "", bool archive = false)
        {
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            List<TypeProduct> typeProduct = new List<TypeProduct>();

            ViewBag.Archive = archive;
            typeProduct = (archive == false) ? db.TypeProducts.Where(f => f.Archive == false).ToList() : db.TypeProducts.Where(f => f.Archive == true).ToList();

            if (filterName != "")
            {
                typeProduct = typeProduct.Where(n => n.Name.Contains(filterName)).ToList();
            }
            return View(typeProduct.OrderBy(c => c.Name).ToPagedList(pageNumber, pageSize));
        }
        public JsonResult CheckName(string Name, int? Id)
        {
            return Json(!db.Products.Any(m => m.Name == Name && m.Id != Id), JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}