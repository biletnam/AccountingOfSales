using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountingOfSales.Models.ViewModel;
using AccountingOfSales.Models;
using System.Web.Security;
using AccountingOfSales.Models.DAL;

namespace AccountingOfSales.Controllers
{    
    public class AccountController : Controller
    {
        SalesDbContext db = new SalesDbContext();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                string hashPassword = CryptHelper.CreateHashMD5(model.Password);
                User user = db.Users.FirstOrDefault(u => u.Login == model.Login && u.Password == hashPassword);

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Login, model.RememberMe);
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }

        [Auth (Roles = "admin")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User newUser = new User();
                Role role = db.Roles.FirstOrDefault(u => u.Name == "user");

                string hashPassword = CryptHelper.CreateHashMD5(model.Password);
                newUser.Login = model.Login;
                newUser.FIO = model.FIO;
                newUser.Password = hashPassword;
                newUser.Roles.Add(role);

                db.Users.Add(newUser);
                db.SaveChanges();

                newUser = db.Users.Where(u => u.Login == model.Login && u.Password == hashPassword).FirstOrDefault();
                // если пользователь удачно добавлен в бд, то создаем куки
                if (newUser != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Login, true);
                    return RedirectToAction("Index", "Product");
                }
                else
                    return HttpNotFound();
            }
            return View(model);
        }
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Product");
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