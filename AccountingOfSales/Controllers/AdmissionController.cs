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
        public ActionResult Index(int? page, DateTime? filterDateAdmissionFrom, DateTime? filterDateAdmissionTo, int? filterUser, int? filterProvider)
        {
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            List<Admission> admissions = new List<Admission>();
            
            //находим дату последних 3 месяцев, от текущей, чтобы ограничить поступления 3 последними месяцами
            DateTime last3Months = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMonths(-3);

            //добавляем первую строку к поставщикам, чтобы был выбор НЕ поставщика
            List<Provider> providers = new List<Provider>();
            providers.Add(new Provider() {Id=0, Name="Выберите поставщика", City="", Archive=false });
            providers.AddRange(db.Providers.OrderBy(n => n.Name));

            List<User> users = new List<User>();
            users.Add(new User() { Id=0, Login="Выберите пользователя" });
            users.AddRange(db.Users.OrderBy(n => n.Login));

            ViewBag.Providers = new SelectList(providers, "Id", "Name");
            ViewBag.Users = new SelectList(users, "Id", "Login");

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

            if (filterProvider != null && filterProvider != 0)
                admissions = admissions.Where(u => u.ProviderId == filterProvider).ToList();

            if (filterUser != null && filterUser != 0)
                admissions = admissions.Where(u => u.User.Id == filterUser).ToList();

            return View(admissions.OrderByDescending(d => d.AdmissionDate).ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdmissionDate, ProviderId, AdditionalCosts, TradePrice, ProductId, Count")] Admission admission)
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