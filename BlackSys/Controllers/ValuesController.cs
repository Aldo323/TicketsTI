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
    public class ValuesController : ApiController
    {    
            List<Solicitudes> model;
        public IHttpActionResult Get(string gard)
            {
                using (BlackSysEntities dc = new BlackSysEntities())
                {
                    dc.Configuration.LazyLoadingEnabled = false;
                    int value; 
                    if(int.TryParse(gard, out value))
                    {
                        model = dc.Solicitudes.Where(x => x.IdSolicitud == value).ToList();
                    }
                    else
                    {
                        model = dc.Solicitudes.ToList();
                    }
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

