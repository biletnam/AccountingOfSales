using AccountingOfSales.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AccountingOfSales.Models.ViewModel;
using System.Net;
using System.Web.Security;
using AccountingOfSales.Models.DAL;

namespace AccountingOfSales.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        SalesDbContext db = new SalesDbContext();

        [Auth(Roles = "admin")]
        public ActionResult Index(bool archive = false)
        {
            List<User> users = new List<User>();

            Session["UserAdminFromIndex"] = true;    //чтобы админ перемещался к списку пользователей, а не на страницу с продуктами, после изменения пароля или редактирования пользователя

            ViewBag.Archive = archive;

            users = db.Users.Where(a => a.Archive == archive).OrderBy(l => l.Login).ToList();

            return View(users);
        }

        public ActionResult ChangePassword(string login)
        {
            ViewBag.LoginUser = login;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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

                    var userAdmin = Session["UserAdminFromIndex"];
                    if (userAdmin != null && (bool)userAdmin)
                    {
                        Session["UserAdminFromIndex"] = null;
                        return RedirectToAction("Index");
                    }
                    else
                        return RedirectToAction("Index", "Product");
                }
                else
                    return HttpNotFound();
            }

            return View(model);
        }

        public ActionResult EditUser(string login)
        {
            if(login == "")
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            User user = db.Users.FirstOrDefault(l => l.Login == login);

            if (user != null)
            {
                EditUserViewModel userModel = new EditUserViewModel() { Id = user.Id, Login = user.Login, FIO = user.FIO };
                return View(userModel);
            }
            else
                return HttpNotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
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


                    var userAdmin = Session["UserAdminFromIndex"];
                    if (userAdmin != null && (bool)userAdmin)
                    {
                        Session["UserAdminFromIndex"] = null;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        FormsAuthentication.SetAuthCookie(user.Login, false);
                        return RedirectToAction("Index", "Product");
                    }
                }
                else
                    return HttpNotFound();
            }

            return View(newUser);
        }

        public ActionResult Archive(int? id, bool unarchive = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            User user = db.Users.FirstOrDefault(i => i.Id == id);

            if (user == null)
            {
                return HttpNotFound();
            }

            user.Archive = (unarchive == false) ? true : false;
            db.SaveChanges();

            return (unarchive == false) ? RedirectToAction("Index") : RedirectToAction("Index", new { archive = true });
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