using AccountingOfSales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountingOfSales.Models.ViewModel;
using System.Net;
using System.Web.Security;

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

        public ActionResult EditUser(string Login)
        {
            if(Login == "")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            User user = db.Users.FirstOrDefault(l => l.Login == Login);

            if (user != null)
            {
                EditUserViewModel userModel = new EditUserViewModel() { Id = user.Id, Login = user.Login, FIO = user.FIO };
                return View(userModel);
            }
            else
                return HttpNotFound();
        }
        [HttpPost]
        public ActionResult EditUser(EditUserViewModel newUser)
        {
            if (ModelState.IsValid)
            {
                User user = db.Users.FirstOrDefault(l => l.Id == newUser.Id);

                if (user != null)
                {
                    user.FIO = newUser.FIO;
                    user.Login = newUser.Login;
                    db.SaveChanges();

                    FormsAuthentication.SetAuthCookie(user.Login, false);
                    return RedirectToAction("Index", "Product");
                }
                else
                    return HttpNotFound();
            }

            return View(newUser);
        }

        public JsonResult CheckLogin(string Login, int? Id)
        {
            return Json(!db.Users.Any(m => m.Login == Login && m.Id != Id), JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}