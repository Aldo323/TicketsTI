using BlackSys.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Web.Http;

namespace BlackSys.Controllers
{
    [Authorize]
    public class BellnotesController : ApiController
    {
      
     
        public IHttpActionResult Get()
        {
            string usuario = User.Identity.GetUserName().ToString();
            BlackSysEntities db = new BlackSysEntities();
            IEnumerable<Notificacion> Informe = db.Database.SqlQuery<Notificacion>("SP_Notificacion @email ='"+usuario+"'").ToList();
            return Json(new { Informe });

        }

    }
}
