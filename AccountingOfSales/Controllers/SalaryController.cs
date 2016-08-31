using AccountingOfSales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using AccountingOfSales.Models.Entities;

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

            List<User> users = new List<User>();
            users.Add(new User() { Login = "Выберите пользователя" });
            users.AddRange(db.Users.OrderBy(n => n.Login));

            ViewBag.Last3Months = last3Months.ToShortDateString();
            ViewBag.TodayDate = DateTime.Now.ToShortDateString();
            ViewBag.Users = new SelectList(users, "Login", "Login");

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
            List<IGrouping<DateTime, Sale>> sales = db.Sales.Where(d => d.SaleDate <= newSalary.EndDate).Where(a => a.ACC == false).
                Where(u => u.User.Login == User.Identity.Name).GroupBy(d => d.SaleDate).ToList();
            if(sales.Count == 0)
                ModelState.AddModelError("", "За данный период не было продаж");
            
            //коллекция Group и дата Key
            foreach(var group in sales)
            {
                //totalSumm
                string bla = "";
            }

            return View();
        }
    }
}