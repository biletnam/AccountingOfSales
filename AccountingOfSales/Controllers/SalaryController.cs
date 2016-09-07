using AccountingOfSales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using AccountingOfSales.Models.DAL;

namespace AccountingOfSales.Controllers
{
    [Authorize]
    public class SalaryController : Controller
    {
        SalesDbContext db = new SalesDbContext();

        public ActionResult Index(int? page, DateTime? filterCreateDateFrom, DateTime? filterCreateDateTo, string filterUserLogin = "")
        {
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            List<Salary> salaries = new List<Salary>();
            
            bool roleAdmin = UserEntities.IsInRole(User.Identity.Name, "admin");

            DateTime last3Months = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddMonths(-3);
            
            ViewBag.Last3Months = last3Months.ToShortDateString();
            ViewBag.TodayDate = DateTime.Now.ToShortDateString();
            ViewBag.Users = new SelectList(ListsForFilters.Users, "Login", "Login");

            if(roleAdmin)
            {
                if(filterUserLogin != "" && filterUserLogin != "Выберите пользователя")
                {
                    salaries = SalaryEntities.GetSalaries(filterCreateDateFrom, filterCreateDateTo, filterUserLogin);
                    return View(salaries.OrderByDescending(c => c.CreateDate).ToPagedList(pageNumber, pageSize));
                }
                salaries = SalaryEntities.GetSalaries(filterCreateDateFrom, filterCreateDateTo);
            }
            else
                salaries = SalaryEntities.GetSalaries(filterCreateDateFrom, filterCreateDateTo, User.Identity.Name);

            return View(salaries.OrderByDescending(c => c.CreateDate).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EndDate")] Salary newSalary)
        {
            if (ModelState.IsValid)
            {
                List<IGrouping<DateTime, Sale>> sales = db.Sales.Where(d => d.SaleDate <= newSalary.EndDate).Where(si => si.SalaryId == null).
                Where(u => u.User.Login == User.Identity.Name).GroupBy(d => d.SaleDate).ToList();

                if (sales.Count == 0)
                    ModelState.AddModelError("", "За данный период не было продаж");

                User user = UserEntities.GetUserByName(User.Identity.Name);

                var firstSale = sales.First();  //получаем дату с

                List<Return> returns = db.Returns.Where(d => d.ReturnDate >= firstSale.Key).Where(d => d.ReturnDate <= newSalary.EndDate).
                    Where(u => u.User.Login == User.Identity.Name).Where(si => si.SalaryId == null).ToList();
                
                int totalPrice = 0; //общая сумма продаж
                int totalReturnPrice = 0;   //общая сумма возвратов     

                if (returns.Count != 0)
                    totalReturnPrice = returns.Sum(p => p.Price);

                //находим общую сумму продаж
                foreach (var groupSales in sales)
                {
                    totalPrice += groupSales.Sum(p => p.SalePrice);
                }

                newSalary.CreateDate = DateTime.Now;
                newSalary.StartDate = firstSale.Key;
                newSalary.UserId = user.Id;
                newSalary.Price = (totalPrice - totalReturnPrice) * Convert.ToInt32(Reference.PercentageOfSales) / 100 + (Convert.ToInt32(Reference.RateForOutput) * sales.Count);

                db.Salaries.Add(newSalary);
                db.SaveChanges();

                foreach (var groupSales in sales)
                {
                    foreach (var saleInGroup in groupSales)
                    {
                        saleInGroup.SalaryId = newSalary.Id;
                    }
                }

                if (returns.Count != 0)
                {
                    foreach (var returnInGroup in returns)
                    {
                        returnInGroup.SalaryId = newSalary.Id;
                    }
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Details(int id)
        {
            Salary salary = db.Salaries.Where(i => i.Id == id).FirstOrDefault();

            if (salary != null)
                return View(salary);
            else
                return HttpNotFound();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}