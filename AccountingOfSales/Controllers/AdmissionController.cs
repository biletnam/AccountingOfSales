using AccountingOfSales.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountingOfSales.Models.DAL;
using AccountingOfSales.Models.ViewModel;

namespace AccountingOfSales.Controllers
{
    [Authorize]
    public class AdmissionController : Controller
    {
        SalesDbContext db = new SalesDbContext();

        [Auth (Roles = "admin")]
        public ActionResult Index(int? page, DateTime? filterDateAdmissionFrom, DateTime? filterDateAdmissionTo, int? filterUser, int? filterProvider)
        {
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            List<Admission> admissions = AdmissionEntities.GetAdmissions(filterDateAdmissionFrom, filterDateAdmissionTo, filterUser, filterProvider);
            
            ViewBag.Providers = new SelectList(ListsForFilters.Providers, "Id", "Name");
            ViewBag.Users = new SelectList(ListsForFilters.Users, "Id", "Login");

            return View(admissions.OrderByDescending(d => d.AdmissionDate).ThenByDescending(d => d.CreateDate).ToPagedList(pageNumber, pageSize));
        }
        
        public ActionResult Create()
        {
            Session["CreatedAdmissions"] = null;

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
        public ActionResult Create([Bind(Include = "PriceOtherCosts, CommentOtherCosts")]AdmissionCreateViewModels model)
        {
            if (ModelState.IsValid)
            {
                if (Session["CreatedAdmissions"] != null)
                {
                    int summCount = (Session["CreatedAdmissions"] as List<Admission>).Sum(c => c.Count);

                    foreach (var newAdmission in (Session["CreatedAdmissions"] as List<Admission>))
                    {
                        //создать поступление
                        Admission admission = new Admission();

                        admission.AdmissionDate = newAdmission.AdmissionDate;
                        admission.CreateDate = newAdmission.CreateDate;
                        admission.ProviderId = newAdmission.ProviderId;
                        admission.UserId = newAdmission.UserId;
                        admission.TradePrice = newAdmission.TradePrice;
                        admission.ProductId = newAdmission.ProductId;
                        admission.Count = newAdmission.Count;

                        //если есть прочие расходы вычислить доп. расходы
                        if (model.PriceOtherCosts != null)
                            admission.AdditionalCosts = model.PriceOtherCosts / summCount;

                        db.Admissions.Add(admission);

                        //изменить количество у товара
                        Product product = db.Products.Where(i => i.Id == newAdmission.ProductId).FirstOrDefault();
                        if(product != null)
                        {
                            int newCount = product.Count + newAdmission.Count;
                            product.Count = newCount;
                        }
                        else
                            return HttpNotFound();
                    }

                    //создать прочие расходы
                    if (model.PriceOtherCosts != null)
                    {
                        OtherCosts otherCosts = new OtherCosts();
                        otherCosts.CreateDate = DateTime.Now;
                        otherCosts.CostsDate = (Session["CreatedAdmissions"] as List<Admission>).First().AdmissionDate;
                        otherCosts.Price = (int)model.PriceOtherCosts;
                        otherCosts.Comment = model.CommentOtherCosts != null ? model.CommentOtherCosts : null;
                        otherCosts.Admission = true;
                        db.OtherCosts.Add(otherCosts);
                    }
                    db.SaveChanges();
                }
                else
                    return RedirectToAction("Create");

                return RedirectToAction("Index");
            }

            return RedirectToAction("Create");
        }

        [HttpPost]
        public ActionResult ListAddAdmissions([Bind(Include = "AdmissionDate, ProviderId, AdditionalCosts, TradePrice, ProductId, Count")] Admission newAdmission)
        {
            if (ModelState.IsValid)
            {
                List<Admission> createdAdmissions = new List<Admission>();
                User user = db.Users.Where(n => n.Login == User.Identity.Name).FirstOrDefault();

                Admission admission = new Admission();
                admission.AdmissionDate = newAdmission.AdmissionDate;
                admission.CreateDate = DateTime.Now;
                admission.ProviderId = newAdmission.ProviderId;
                admission.UserId = user.Id;
                admission.TradePrice = newAdmission.TradePrice;
                admission.ProductId = newAdmission.ProductId;
                admission.Product = db.Products.Where(i => i.Id == newAdmission.ProductId).FirstOrDefault();
                admission.Count = newAdmission.Count;                

                if (Session["CreatedAdmissions"] == null)
                {
                    createdAdmissions.Add(admission);
                    Session["CreatedAdmissions"] = createdAdmissions;
                }
                else
                    (Session["CreatedAdmissions"] as List<Admission>).Add(admission);


                return PartialView(Session["CreatedAdmissions"]);
            }

            return RedirectToAction("Create");
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