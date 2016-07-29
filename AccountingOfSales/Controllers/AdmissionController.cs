using AccountingOfSales.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AccountingOfSales.Controllers
{
    public class AdmissionController : Controller
    {
        SalesDbContext db = new SalesDbContext();
        public ActionResult Index(int? page, DateTime? filterDateAdmissionFrom, DateTime? filterDateAdmissionTo)
        {
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            List<Admission> admissions = new List<Admission>();

            DateTime last3Month = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMonths(-3);

            admissions = db.Admissions.Where(d => d.AdmissionDate > last3Month).ToList();

            return View(admissions.OrderByDescending(d => d.AdmissionDate).ToPagedList(pageNumber, pageSize));
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}