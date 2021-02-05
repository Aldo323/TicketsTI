    using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using BlackSys.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace BlackSys.Controllers
{
    [Authorize]
    public class NomAgentesController : ApiController
    {
        List<AspNetRoles> model;
        List<AspNetUserRoles> roles;
        List<String> lst = new List<String>();
        public IHttpActionResult Get()
        {

            string emailuser = @"Select Email from AspNetUsers where Id IN (Select UserId from AspNetUserRoles WHERE RoleId = (Select Id from AspNetRoles WHERE Name = 'Sistemas' ) )";
            string conStr = ConfigurationManager.ConnectionStrings["BlackSysConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conStr))
            {
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }
                SqlCommand cmd_2 = new SqlCommand(emailuser, con);
                using (SqlDataReader reader_2 = cmd_2.ExecuteReader())
                {
                    if (reader_2.Read())
                    {
                        while(reader_2.Read())
                        {
                            lst.Add(reader_2[0].ToString());
                        }
                    

                        ///notificationHub.Clients.All.notify(contact);

                        return Json(new { lst });
                    }

                }
            }
            return Json(new { data = "" });
            
        }


        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public async Task<IHttpActionResult> Post([FromBody]RangoSolicitudes value)
        {
            BlackSysEntities db = new BlackSysEntities();
            IEnumerable<RangoFechas> Informe = db.Database.SqlQuery<RangoFechas>("SP_Reportig_ActivityTickets @DateStaring ='" + value.Fecha_inicio + "', @DateFiniched = '" + value.Fecha_fin + "'").ToList();

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string outputOfInts = serializer.Serialize(Informe);
            //GenerateFromJSONTest jnt = new GenerateFromJSONTest();
            // jnt.Poster(value);


            //return Json(new { outputOfInts });
            return Json(new { outputOfInts });

        }


        // PUT api/values/5
        public void Put(int id, [FromBody] Solicitudes value)
        {
          
            
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
