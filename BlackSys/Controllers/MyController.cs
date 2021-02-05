using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using BlackSys.Models;
using Microsoft.AspNet.Identity;

namespace BlackSys.Controllers
{
    [Authorize]
    public class MyController : ApiController
    {

        public IHttpActionResult Get()
        {
 
            using (BlackSysEntities dc = new BlackSysEntities())
            {
                string usuario = User.Identity.GetUserName().ToString();
                //var roles = ((ClaimsIdentity)User.Identity).Claims
                //.Where(c => c.Type == ClaimTypes.Role)
                //.Select(c => c.Value).ToList();

                //var rol = roles[0].ToString();
                dc.Configuration.LazyLoadingEnabled = false;
                int own = dc.Solicitudes.Where(x => x.Responsable == usuario && x.Estatus == "ABIERTO" ).Count();

                return Json(new { own });

            }
        }
    }

}
