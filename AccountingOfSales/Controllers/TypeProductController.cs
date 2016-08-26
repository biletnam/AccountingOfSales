using AccountingOfSales.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            typeProduct = db.TypeProducts.Where(f => f.Archive == archive).ToList();

            if (filterName != "")
            {
                typeProduct = typeProduct.Where(n => n.Name.Contains(filterName)).ToList();
            }
            return View(typeProduct.OrderBy(c => c.Name).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] TypeProduct typeProduct)
        {
            if (ModelState.IsValid)
            {                
                db.TypeProducts.Add(typeProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TypeProduct typeProduct = db.TypeProducts.Find(id);

            if (typeProduct == null)
                return HttpNotFound();

            return View(typeProduct);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Name")] TypeProduct editTypeProduct)
        {
            if (ModelState.IsValid)
            {
                TypeProduct typeProduct = db.TypeProducts.Find(editTypeProduct.Id);

                if (typeProduct == null)
                    return HttpNotFound();

                typeProduct.Name = editTypeProduct.Name;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(editTypeProduct);
        }

        public ActionResult Archive(int? id, bool unarchive = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TypeProduct typeProduct = db.TypeProducts.Find(id);

            if (typeProduct == null)
            {
                return HttpNotFound();
            }

            typeProduct.Archive = (unarchive == false) ? true : false;
            db.SaveChanges();

            return (unarchive == false) ? RedirectToAction("Index") : RedirectToAction("Index", new { archive = true });
        }

        public JsonResult CheckName(string Name, int? Id)
        {
            return Json(!db.TypeProducts.Any(m => m.Name == Name && m.Id != Id), JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}