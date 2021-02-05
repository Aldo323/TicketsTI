using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using BlackSys.Models;


namespace BlackSys.Filters
{
    public class BasicAuthenticationFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if(actionContext.Request.Headers.Authorization == null )
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized);
            }
            else
            {
                {
                    string authonticationToken = actionContext.Request.Headers.Authorization.Parameter; // BASE64 encodedstring

                    string decodedAuthToken = Encoding.UTF8.GetString(Convert.FromBase64String(authonticationToken));
                    string[] usernamePasswordArray = decodedAuthToken.Split(':');

                    string username = usernamePasswordArray[0];
                    string password = usernamePasswordArray[1];

                    //if(AppSecurity.ValidateUserCredentials(username, password) == true)
                    {
                        //string[] rolex = { AspNetUsers.};
                    }
                }
            }
        }
    }
}