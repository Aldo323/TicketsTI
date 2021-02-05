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
    public class BusquedaActivoController : ApiController
    {
   
        public IHttpActionResult Get(string search)
        {
            BlackSysEntities db = new BlackSysEntities();
            List<tab_Activos> json = db.Database.SqlQuery<tab_Activos>("SP_SearchAllActivos @valor='" + search + "'").ToList();
            //int cantidad = data.Count();
            var dato = JsonConvert.SerializeObject(json);
            var status = JsonConvert.DeserializeObject<List<Dictionary<string, string>>>(dato);
            return Json(new { data = status });

        }

        // GET: 
        
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
