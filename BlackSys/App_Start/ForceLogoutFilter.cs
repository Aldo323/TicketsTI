using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using BlackSys.Models;

namespace BlackSys.Filters
{
    public class ForceLogoutFilter : ActionFilterAttribute
    {
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }


        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var UserManager = filterContext.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                //var user = UserManager.FindByEmailAsync(HttpContext.Current.User.Identity.Name).Result; 
                var user = UserManager.FindByNameAsync(HttpContext.Current.User.Identity.Name).Result; 
                
            }
            //else
            //{
            //    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            //    filterContext.HttpContext.Session.Clear();
            //    filterContext.HttpContext.Session.Abandon();
            //    filterContext.Result = new RedirectToRouteResult(
            //                new RouteValueDictionary    {{ "Controller", "Account" },
            //                                                { "Action", "Login" } });
            //}

            base.OnActionExecuting(filterContext);
        }

    }

}
