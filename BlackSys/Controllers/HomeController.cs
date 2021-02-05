using API_TEST.Controllers;
using BlackSys.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BlackSys.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private BlackSysEntities db = new BlackSysEntities();
        public ActionResult Index()

        {
            string correo = User.Identity.GetUserName().ToString();
            string vistamg = db.Database.SqlQuery<string>("Sp_loadStringMenu  @User ='" + correo + "', @idMenu= 1 ").FirstOrDefault();
             if (vistamg == null)
            {

                return View("~/Views/Error/Denegado.cshtml");
            }
            return View(vistamg);
         
         //To Know role assigned
         //   var roles = ((ClaimsIdentity)User.Identity).Claims
         //.Where(c => c.Type == ClaimTypes.Role)
         //.Select(c => c.Value).ToList();
         //   var rol = roles[0].ToString();
               
               

        }

        public ActionResult Abierto()
        {
            string correo = User.Identity.GetUserName().ToString();
            string vistamg = db.Database.SqlQuery<string>("Sp_loadStringMenu  @User ='" + correo + "', @idMenu= 5 ").FirstOrDefault();
            if (vistamg == null)
            {

                return View("~/Views/Error/Denegado.cshtml");
            }
            return View(vistamg);

        }

        public ActionResult NC()
        {

            List<Responsable> RespList = db.Responsables.ToList();
            List<EstadosTickets> EstadoList = db.EstadosTickets.ToList();

            ViewBag.RespList = new SelectList(RespList, "Id_responsable", "Responsable_name");
            ViewBag.EstadoList = new SelectList(EstadoList, "Id_Estado", "Estado");
            //var estatus = db.Solicitudes.Where(x => x.IdSolicitud == id).Select(x => new { x.Estatus });
            Session["test"] = 23;
            string correo = User.Identity.GetUserName().ToString();
            string vistamg = db.Database.SqlQuery<string>("Sp_loadStringMenu  @User ='" + correo + "', @idMenu= 7 ").FirstOrDefault();
            if (vistamg == null)
            {

                return View("~/Views/Error/Denegado.cshtml");
            }
            return View(vistamg);

        }

        public ActionResult Cerrado()
        {
            string correo = User.Identity.GetUserName().ToString();
            string vistamg = db.Database.SqlQuery<string>("Sp_loadStringMenu  @User ='" + correo + "', @idMenu= 9 ").FirstOrDefault();
            if (vistamg == null)
            {

                return View("~/Views/Error/Denegado.cshtml");
            }
            return View(vistamg);

        }
        public ActionResult Asignado()
        {
            string correo = User.Identity.GetUserName().ToString();
            string vistamg = db.Database.SqlQuery<string>("Sp_loadStringMenu  @User ='" + correo + "', @idMenu= 11 ").FirstOrDefault();
            if (vistamg == null)
            {

                return View("~/Views/Error/Denegado.cshtml");
            }
            return View(vistamg);

        }

        public ActionResult Vencido()
        {
            string correo = User.Identity.GetUserName().ToString();
            string vistamg = db.Database.SqlQuery<string>("Sp_loadStringMenu  @User ='" + correo + "', @idMenu= 12 ").FirstOrDefault();
            if (vistamg == null)
            {

                return View("~/Views/Error/Denegado.cshtml");
            }
            return View(vistamg);

        }

        public ActionResult Reportes()
        {
            string correo = User.Identity.GetUserName().ToString();
            string vistamg = db.Database.SqlQuery<string>("Sp_loadStringMenu  @User ='" + correo + "', @idMenu= 14 ").FirstOrDefault();
            if (vistamg == null)
            {

                return View("~/Views/Error/Denegado.cshtml");
            }
            return View(vistamg);

        }

        public ActionResult Agente()
        {
            string correo = User.Identity.GetUserName().ToString();
            string vistamg = db.Database.SqlQuery<string>("Sp_loadStringMenu  @User ='" + correo + "', @idMenu= 15 ").FirstOrDefault();
            if (vistamg == null)
            {

                return View("~/Views/Error/Denegado.cshtml");
            }
            return View(vistamg);

        }
        public ActionResult Contact()
        {
            return View();
        }


        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [ChildActionOnly]
        public async Task<ActionResult> LoadMenu()
        {

            string email_usr = User.Identity.GetUserName().ToString();
            var user =  UserManager.FindByNameAsync(email_usr);

            var users = (from c in db.AspNetUsers
                         where
                              c.Email.Contains(email_usr)
                         select c).ToList();
            // string s = user.;
            string ls = users[0].Id;

            var menu = from a in db.MenuTemp
                       where a.UserId.Contains(ls)
                       select a;
            List<MenuTemp> li = menu.ToList();
            return View(menu.ToList());
        }

         
        //public ActionResult LoadMenu()
        //{
        //    string user = User.Identity.GetUserName().ToString();
        //    var menu = from a in db.MenuTemp
        //                       select a;

          
        //    return View(menu.ToList());
        //}

        public JsonResult GetNotificationContacts()
        {
            var notificationRegisterTime = Session["LastUpdated"] != null ? Convert.ToDateTime(Session["LastUpdated"]) : DateTime.Now;
            NotificationComponent NC = new NotificationComponent();
            //var list = NC.GetContacts(notificationRegisterTime);
            var list = NC.GetContacts(DateTime.Now);
            //update session here for get only new added contacts (notification)
            Session["LastUpdate"] = DateTime.Now;
            return new JsonResult { Data ="", JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}