using AccountingOfSales.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountingOfSales.Models.Entities;

namespace AccountingOfSales.Controllers
{
    public class AdmissionController : Controller
    {
        SalesDbContext db = new SalesDbContext();
        public ActionResult Index(int? page, DateTime? filterDateAdmissionFrom, DateTime? filterDateAdmissionTo, int? filterUser, int? filterProvider)
        {
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            List<Admission> admissions = AdmissionEntities.GetAdmissions(filterDateAdmissionFrom, filterDateAdmissionTo, filterUser, filterProvider);
            
            //добавляем нового пустого поставщика, чтобы был выбор НЕ поставщика
            List<Provider> providers = new List<Provider>();
            providers.Add(new Provider() {Id=0, Name="Выберите поставщика", City="", Archive=false });
            providers.AddRange(db.Providers.OrderBy(n => n.Name));

            List<User> users = new List<User>();
            users.Add(new User() { Id=0, Login="Выберите пользователя" });
            users.AddRange(db.Users.OrderBy(n => n.Login));

            ViewBag.Providers = new SelectList(providers, "Id", "Name");
            ViewBag.Users = new SelectList(users, "Id", "Login");

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