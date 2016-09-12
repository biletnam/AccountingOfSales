using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AccountingOfSales.Models.DAL
{
    public class AuthAttribute : AuthorizeAttribute
    {
        private string[] allowedUsers = new string[] { };
        private string[] allowedRoles = new string[] { };

        public AuthAttribute()
        { }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!String.IsNullOrEmpty(base.Users))
            {
                allowedUsers = base.Users.Split(new char[] { ',' });
                for (int i = 0; i < allowedUsers.Length; i++)
                {
                    allowedUsers[i] = allowedUsers[i].Trim();
                }
            }
            if (!String.IsNullOrEmpty(base.Roles))
            {
                allowedRoles = base.Roles.Split(new char[] { ',' });
                for (int i = 0; i < allowedRoles.Length; i++)
                {
                    allowedRoles[i] = allowedRoles[i].Trim();
                }
            }

            return httpContext.Request.IsAuthenticated &&
                 User(httpContext) && Role(httpContext);
        }

        private bool User(HttpContextBase httpContext)
        {
            if (allowedUsers.Length > 0)
            {
                return allowedUsers.Contains(httpContext.User.Identity.Name);
            }
            return true;
        }

        private bool Role(HttpContextBase httpContext)
        {
            if (allowedRoles.Length > 0)
            {
                for (int i = 0; i < allowedRoles.Length; i++)
                {
                    if (UserEntities.IsInRole(httpContext.User.Identity.Name, allowedRoles[i]))
                        return true;
                }
                return false;
            }
            return true;
        }

        //public override void OnAuthorization(AuthorizationContext filterContext)
        //{
        //    // если пользователь не принадлежит роли admin, то он перенаправляется на Home/About
        //    bool auth = UserEntities.IsInRole(filterContext.HttpContext.User.Identity.Name, "admin");
        //    if (!auth)
        //    {
        //        filterContext.Result = new RedirectToRouteResult(
        //            new System.Web.Routing.RouteValueDictionary {
        //        { "controller", "Account" }, { "action", "Login" }
        //        });
        //    }
        //}
    }
}
