using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BlackSys.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace BlackSys.Controllers
{

    public class SendUIdController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();
        // GET: Engineering


       
        public ActionResult Activar(string id)
        {
            try
            {
                IEnumerable<AspNetUsers> Informe = db.Database.SqlQuery<AspNetUsers>("sp_UserConfirm  @UserId = '" + id + "'").ToList();
                //var result = await UserManager.ConfirmEmailAsync(userId, code);
                return RedirectToAction("ConfirmEmail", "Account");
            }
            catch
            {
                return View("Error");
            }
        }


    }
}