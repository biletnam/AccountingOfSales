using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountingOfSales.Models;
using PagedList.Mvc;
using PagedList;
using System.IO;

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

            return View(products.OrderByDescending(n => n.Count).ThenBy(c => c.Name).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            //SelectList providers = new SelectList(db.Providers.OrderBy(n => n.Name), "Id", "Name");
            //ViewBag.Providers = providers;
            //для заполнения выпадающих списков
            ViewBag.Providers = new SelectList(db.Providers.OrderBy(n => n.Name), "Id", "Name");
            ViewBag.TypeProducts = new SelectList(db.TypeProducts.OrderBy(n => n.Name), "Id", "Name");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, Model, Color, Size, RetailPrice, ProviderId, TypeProductId")] Product product, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    Image dbImage = new Image();
                    dbImage.Extension = Path.GetExtension(image.FileName).TrimStart(new char[] { '.' });
                    db.Images.Add(dbImage);
                    db.SaveChanges();

                    image.SaveAs(Server.MapPath("~/Images/" + dbImage.Name));

                    product.Image = dbImage;
                }

                product.CreateDate = DateTime.Now;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        public JsonResult CheckName(string Name)
        {
            if (db.Products.Any(m => m.Name == Name))
                return Json(false, JsonRequestBehavior.AllowGet);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}