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
     public class OwnController : ApiController
    {
        List<Solicitudes> model;
        public IHttpActionResult Get()
        {
            using (BlackSysEntities dc = new BlackSysEntities())
            {
                string usuario = User.Identity.GetUserName().ToString();

                    model = dc.Solicitudes.Where(x => x.Responsable == usuario && x.Estatus == "ABIERTO").ToList();


                //  var model = dc.Bitacora.Where(x => x.Correo == gard + "@intelexion.com").ToList();

                // var json = new JavaScriptSerializer().Serialize(dc.Bitacora.Where(x => x.Correo == gender + "@intelexion.com").ToList());
                //   return  Request.CreateResponse(HttpStatusCode.OK, model, Configuration.Formatters.JsonFormatter);


                //return Json(model,settings);
                return Json(new { data = model });
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
