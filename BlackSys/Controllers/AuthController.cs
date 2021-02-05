using API_TEST.Controllers;
using BlackSys.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Web.Http;

namespace BlackSys.Controllers
{
    [Authorize]
    public class AuthController : ApiController
    {
        string validacion;
        public IHttpActionResult Get()
        {
            if (User.Identity.IsAuthenticated)
            {
                validacion = "true";
            }
            else
            {
                validacion = "false";
            }
        
            return Json(new { validacion });

        }

    }
}
