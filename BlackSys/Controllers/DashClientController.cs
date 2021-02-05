using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlackSys.Models;
using Microsoft.AspNet.Identity;

namespace BlackSys.Controllers
{
    [Authorize]
    public class DashClientController : ApiController
    {
        public IHttpActionResult Get()
        {
            string usuario = User.Identity.GetUserName().ToString();
            //NotificationHub NC = new NotificationHub();
            //var currentTime = DateTime.Now;
            //NC.RegisterNotification(currentTime, usuario);
            using (BlackSysEntities dc = new BlackSysEntities())
            {
                int client = dc.Solicitudes.Where(x => x.Correo == usuario).Count();
                int abiertos = dc.Solicitudes.Where(x => x.Correo == usuario && x.Estatus == "Abierto").Count();
                int cerrado = dc.Solicitudes.Where(x => x.Correo == usuario && x.Estatus == "Cerrado").Count();
                int nc = dc.Solicitudes.Where(x => x.Correo == usuario && x.Estatus == "N.C").Count();
                int vencidos = dc.Solicitudes.Where(x => x.Correo == usuario && x.Vencido == 1 && x.Estatus == "Abierto").Count();
                int r = 3;
                return Json(new {cerrado,abiertos,nc,vencidos});

            }

        }
    }
}




                