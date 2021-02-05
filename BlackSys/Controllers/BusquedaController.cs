using BlackSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Microsoft.AspNet.SignalR.Json;
using System.Text.RegularExpressions;

namespace BlackSys.Controllers
{
    [Authorize]
    public class BusquedaController : ApiController
    {
   
       public IHttpActionResult Get(string search)
        {
            BlackSysEntities db = new BlackSysEntities();
            List<Search> json = db.Database.SqlQuery<Search>("SP_SearchAll @valor='" + search + "'").ToList();
            List<BusquedaDescripcion> li = new List<BusquedaDescripcion>();
            //int cantidad = data.Count();
            var dat = db.Solicitudes.Where(p => p.Descripcion.Contains(search)).Select(p => p.Descripcion).ToList();
            foreach(var m in dat)
            {
                string r = Regex.Replace(m, "<.*?>", String.Empty);
                li.Add(new BusquedaDescripcion() { Descripcion = r});

            }
            var dato = JsonConvert.SerializeObject(li);
            var status = JsonConvert.DeserializeObject<List<string>>(dato);
            return Json(new { data = status });
            

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
