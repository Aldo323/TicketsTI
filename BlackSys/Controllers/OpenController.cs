using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
    public class OpenController : ApiController
    {
        List<Solicitudes> model;
        private BlackSysEntities db = new BlackSysEntities();
        public IHttpActionResult Get()
        {


            //var roles = ((ClaimsIdentity)User.Identity).Claims
            //.Where(c => c.Type == ClaimTypes.Role)
            //.Select(c => c.Value).ToList();

            //var rol = roles[0].ToString();
            string usuario = User.Identity.GetUserName().ToString();
            IEnumerable<Solicitudes> Informe = db.Database.SqlQuery<Solicitudes>("Sp_loadStringMenu  @User ='" + usuario + "', @idMenu= 6 ").ToList();
                
              
               return Json(new { data = Informe });
            
        }

        //public string Nombre()
        //{
        //    string user = User.Identity.GetUserName().ToString();
        //    return user;
        //}

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
