using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlackSys.Models;

namespace BlackSys.Models
{
    [Authorize]
    public class UsersController : ApiController
    {
        List<Mensajes> model;
        public IHttpActionResult Get(string gard)
        {
            using (BlackSysEntities dc = new BlackSysEntities())
            {
                dc.Configuration.LazyLoadingEnabled = false;
                int id_solicitud = int.Parse(gard);
                model = dc.Mensajes.Where(x => x.Id_Solicitud == id_solicitud).ToList();

                return Json(model);
            }
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

