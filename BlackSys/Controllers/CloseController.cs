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
    public class CloseController : ApiController
    {
        private BlackSysEntities db = new BlackSysEntities();
        List<Solicitudes> model;
        public IHttpActionResult Get()
        {
            string usuario = User.Identity.GetUserName().ToString();

      
            IEnumerable<Solicitudes> Informe = db.Database.SqlQuery<Solicitudes>("Sp_loadStringMenu  @User ='" + usuario + "', @idMenu= 10 ").ToList();
            return Json(new { data = Informe });
        }

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
