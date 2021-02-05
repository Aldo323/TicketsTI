using BlackSys.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

namespace BlackSys.Controllers
{
   
    public class UserController : ApiController
    {
        string usuario;
        public IHttpActionResult Get()
        {
            string usuario = User.Identity.GetUserName().ToString();
            var roles = ((ClaimsIdentity)User.Identity).Claims
            .Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value).ToList();

            var rol = roles[0].ToString();
            try
            {
                usuario = User.Identity.GetUserName().ToString();
            }
          catch
            {

            }

            return Json(new { roles = rol });
        //return Json(new { abierto, cerrado, nc });

            
        }

        static async Task<string> GetURI(Uri u)
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {
                HttpResponseMessage result = await client.GetAsync(u);
                if (result.IsSuccessStatusCode)
                {
                    response = await result.Content.ReadAsStringAsync();
                }
            }
            return response;
        }


           }
}
