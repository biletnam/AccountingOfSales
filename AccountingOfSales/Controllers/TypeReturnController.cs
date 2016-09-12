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
    [Authorize]
    public class TypeReturnController : Controller
    {
        SalesDbContext db = new SalesDbContext();

        public ActionResult Index(int? page, string filterName = "", bool archive = false)
        {
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            List<TypeReturn> typeReturn = new List<TypeReturn>();

            ViewBag.Archive = archive;
            typeReturn = db.TypeReturns.Where(f => f.Archive == archive).ToList();

            if (filterName != "")
            {
                typeReturn = typeReturn.Where(n => n.Name.Contains(filterName)).ToList();
            }
            return View(typeReturn.OrderBy(c => c.Name).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] TypeReturn typeReturn)
        {
            if (ModelState.IsValid)
            {
                db.TypeReturns.Add(typeReturn);
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

            TypeReturn typeReturn = db.TypeReturns.Find(id);

            if (typeReturn == null)
                return HttpNotFound();

            return View(typeReturn);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Name")] TypeReturn editTypeReturn)
        {
            if (ModelState.IsValid)
            {
                TypeReturn typeReturn = db.TypeReturns.Find(editTypeReturn.Id);

                if (typeReturn == null)
                    return HttpNotFound();

                typeReturn.Name = editTypeReturn.Name;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(editTypeReturn);
        }
        public ActionResult Archive(int? id, bool unarchive = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TypeReturn typeReturn = db.TypeReturns.Find(id);

            if (typeReturn == null)
            {
                return HttpNotFound();
            }

            typeReturn.Archive = (unarchive == false) ? true : false;
            db.SaveChanges();

            return (unarchive == false) ? RedirectToAction("Index") : RedirectToAction("Index", new { archive = true });
        }
        public JsonResult CheckName(string Name, int? Id)
        {
            return Json(!db.TypeReturns.Any(m => m.Name == Name && m.Id != Id), JsonRequestBehavior.AllowGet);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}