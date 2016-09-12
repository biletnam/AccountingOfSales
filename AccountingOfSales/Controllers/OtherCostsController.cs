using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using AccountingOfSales.Models;
using AccountingOfSales.Models.DAL;

namespace AccountingOfSales.Controllers
{
    [Authorize]
    public class OtherCostsController : Controller
    {
        SalesDbContext db = new SalesDbContext();

        // GET: OtherCosts
        public ActionResult Index(int? page, DateTime? filterDateCostFrom, DateTime? filterDateCostTo)
        {
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            List<OtherCosts> otherCosts = OtherCostsEntities.GetOtherCosts(filterDateCostFrom, filterDateCostTo);

            return View(otherCosts.OrderByDescending(d => d.CostsDate).ThenByDescending(d => d.CreateDate).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CostsDate, Price, Comment")] OtherCosts newOtherCost)
        {
            if (ModelState.IsValid)
            {
                newOtherCost.CreateDate = DateTime.Now;                
                db.OtherCosts.Add(newOtherCost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newOtherCost);
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}