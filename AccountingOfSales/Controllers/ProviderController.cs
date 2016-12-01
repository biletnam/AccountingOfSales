using AccountingOfSales.Models;
using AccountingOfSales.Models.DAL;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AccountingOfSales.Controllers
{
    [Auth (Roles = "admin")]
    public class ProviderController : Controller
    {
        SalesDbContext db = new SalesDbContext();
        // GET: Provider
        public ActionResult Index(int? page, string filterName = "", bool archive = false)
        {
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            List<Provider> providers = new List<Provider>();

            ViewBag.Archive = archive;
            providers = db.Providers.Where(f => f.Archive == archive).ToList();

            if (filterName != "")
            {
                providers = providers.Where(n => n.Name.ToLower().Contains(filterName.ToLower())).ToList();
            }

            return View(providers.OrderBy(c => c.Name).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, City")]Provider provider)
        {
            if (ModelState.IsValid)
            {
                db.Providers.Add(provider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(provider);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Provider provider = db.Providers.Find(id);
            if (provider != null)
                return View(provider);
            else
                return HttpNotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Name, City")]Provider editProvider)
        {
            if (ModelState.IsValid)
            {
                Provider provider = db.Providers.Find(editProvider.Id);

                if (provider == null)
                    return HttpNotFound();

                provider.Name = editProvider.Name;
                provider.City = editProvider.City;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(editProvider);
        }
        public ActionResult Archive(int? id, bool unarchive = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Provider provider = db.Providers.Find(id);

            if (provider == null)
            {
                return HttpNotFound();
            }

            provider.Archive = (unarchive == false) ? true : false;
            db.SaveChanges();

            return (unarchive == false) ? RedirectToAction("Index") : RedirectToAction("Index", new { archive = true });
        }

        public JsonResult CheckName(string Name, int? Id)
        {
            return Json(!db.Providers.Any(m => m.Name == Name && m.Id != Id), JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}