using AccountingOfSales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountingOfSales.Models.ViewModel;

namespace AccountingOfSales.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        SalesDbContext db = new SalesDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.FirstOrDefault(l => l.Login == model.Login);
                if (user != null)
                {
                    string hashPassword = CryptHelper.CreateHashMD5(model.Password);
                    user.Password = hashPassword;
                    db.SaveChanges();

                    return RedirectToAction("Index", "Product");
                }
                else
                    return HttpNotFound();
            }

            return View(model);
        }

        public ActionResult EditUser()
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