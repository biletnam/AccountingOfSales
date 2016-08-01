using AccountingOfSales.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountingOfSales.Models.ViewModel;

namespace AccountingOfSales.Controllers
{
    public class AdmissionController : Controller
    {
        SalesDbContext db = new SalesDbContext();
        public ActionResult Index(int? page, DateTime? filterDateAdmissionFrom, DateTime? filterDateAdmissionTo, string userName)
        {
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            List<Admission> admissions = new List<Admission>();
            
            //находим дату последних 3 месяцев, от текущей, чтобы ограничить поступления 3 последними месяцами
            DateTime last3Months = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMonths(-3);

            if (filterDateAdmissionTo != null)
                filterDateAdmissionTo = filterDateAdmissionTo.Value.AddDays(1); //прибавляем к "дате по" 1 день, чтобы дата была включительно

            if (filterDateAdmissionFrom != null && filterDateAdmissionTo != null)
                admissions = db.Admissions.Where(d => d.AdmissionDate > filterDateAdmissionFrom).Where(d => d.AdmissionDate < filterDateAdmissionTo).ToList();
            else if (filterDateAdmissionFrom != null && filterDateAdmissionTo == null)
                admissions = db.Admissions.Where(d => d.AdmissionDate > filterDateAdmissionFrom).ToList();
            else if (filterDateAdmissionFrom == null && filterDateAdmissionTo != null)
            {
                //находим дату последних 3 месяцев, от "даты по", чтобы опять же ограничить 3 месяцами
                DateTime last3MonthsDateTo = new DateTime(filterDateAdmissionTo.Value.Year, filterDateAdmissionTo.Value.Month, filterDateAdmissionTo.Value.Day).AddMonths(-3);
                admissions = db.Admissions.Where(d => d.AdmissionDate > last3MonthsDateTo).Where(d => d.AdmissionDate < filterDateAdmissionTo).ToList();
            }
            else
                admissions = db.Admissions.Where(d => d.AdmissionDate > last3Months).ToList();

            if (userName != null && userName != "")
                admissions = admissions.Where(u => u.User.Login == userName).ToList();

            return View(admissions.OrderByDescending(d => d.AdmissionDate).ToPagedList(pageNumber, pageSize));
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}