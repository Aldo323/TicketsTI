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
    public class InventarioController : ApiController
    {
        
        private BlackSysEntities db = new BlackSysEntities();
        public IHttpActionResult Get()
        {
           
                string usuario = User.Identity.GetUserName().ToString();
                IEnumerable<tab_Activos> Informe = db.Database.SqlQuery<tab_Activos>("Sp_loadStringMenu  @User ='" + usuario + "', @idMenu= 17 ").ToList();
            if(Informe == null)
            {
                return Json(new { data = "No Data" });
            }
            else
            {
                return Json(new { data = Informe });
            }
                
            
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
