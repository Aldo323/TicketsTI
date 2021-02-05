using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlackSys.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Net;


namespace BlackSys.Filters
{
    /*public class AppSecurity
    {
        private static BlackSysEntities db = new BlackSysEntities();

        public static bool ValidateUserCredentials(string userName, string password)
        {
            ApplicationUserManager userManager = new HttpContextWrapper(HttpContext.Current).GetOwinContext().GetUserManager<ApplicationUserManager>();
            var context = (from u in db. where u.UserName == userName && u.PasswordHash == userManager.PasswordHasher.HashPassword(password) select u).FirstOrDefault();

            if(context == null)
            {
                return false;
            }

            AspNetUsers.UserName = context.UserName;
            
        }
    }*/
}