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
    public class ConversacionController : ApiController
    {
 

        public IHttpActionResult Get(string gard)
        {
            using (BlackSysEntities dc = new BlackSysEntities())
            {

                int id_solicitud = int.Parse(gard);


                BlackSysEntities db = new BlackSysEntities();
                IEnumerable<Mensajes> Informe = db.Database.SqlQuery<Mensajes>("SP_MessageLoad @idsolicitud ='" + id_solicitud + "'").ToList();
                return Json( Informe);

                
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

