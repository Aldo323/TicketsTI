using BlackSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Microsoft.AspNet.SignalR.Json;

namespace BlackSys.Controllers
{
    [Authorize]
    public class ConfirmarController : ApiController
    {
        //  GET: api/Busqueda
        //  GET: api/Busqueda

        public IHttpActionResult Get(string userId)
        {
            BlackSysEntities db = new BlackSysEntities();
            IEnumerable<AspNetUsers> Informe = db.Database.SqlQuery<AspNetUsers>("sp_UserConfirm  @UserId = '" + userId + "'").ToList();
            //int cantidad = data.Count();

            return Json(new { data = "" });

        }

        // GET: api/Busqueda/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Busqueda
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Busqueda/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Busqueda/5
        public void Delete(int id)
        {
        }
    }
}
