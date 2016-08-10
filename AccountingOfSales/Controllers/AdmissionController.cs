using AccountingOfSales.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountingOfSales.Models.Entities;
using AccountingOfSales.Models.ViewModel;

namespace AccountingOfSales.Controllers
{
    public class AdmissionController : Controller
    {
        SalesDbContext db = new SalesDbContext();
        List<Admission> createdAdmissions = new List<Admission>();
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
        [Authorize]
        public ActionResult Create()
        {
            List<Provider> providers = db.Providers.Where(a => a.Archive == false).OrderBy(n => n.Name).ToList();

            int? selectedValue = null; //нал, чтобы списки были пустыми
            if (providers.Count != 0)
                selectedValue = providers.First().Id;

            ViewBag.Providers = new SelectList(providers, "Id", "Name", selectedValue);
            ViewBag.Products = new SelectList(db.Products.Where(i => i.ProviderId == selectedValue).OrderBy(n => n.Name), "Id", "Name");

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AdmissionDate, ProviderId, AdditionalCosts, TradePrice, ProductId, Count")] AdmissionCreateViewModels newAdmission)
        {
            return View();
        }

        [HttpPost]
        public ActionResult ListAddAdmissions([Bind(Include = "AdmissionDate, ProviderId, AdditionalCosts, TradePrice, ProductId, Count")] AdmissionCreateViewModels newAdmission)
        {
            Admission admission = new Admission();
            admission.CreateDate = DateTime.Now;
            admission.AdmissionDate = newAdmission.AdmissionDate;
            admission.Provider = db.Providers.Where(i => i.Id == newAdmission.ProviderId).FirstOrDefault();
            admission.User = db.Users.Where(n => n.Login == User.Identity.Name).FirstOrDefault();
            admission.TradePrice = newAdmission.TradePrice;
            admission.Product = db.Products.Where(i => i.Id == newAdmission.ProductId).FirstOrDefault();
            admission.Count = newAdmission.Count;

            createdAdmissions.Add(admission);

            return PartialView(createdAdmissions);
        }
        
        public ActionResult GetProducts(int id)
        {
            return PartialView(db.Products.Where(c => c.ProviderId == id).OrderBy(n => n.Name).ToList());
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}