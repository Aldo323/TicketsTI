using System.Web.Mvc;
using BlackSys.Filters;
namespace BlackSys
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ForceLogoutFilter());
            filters.Add(new VerificarCorreo());
            filters.Add(new HandleErrorAttribute());
            filters.Add(new HandleAntiforgeryTokenErrorAttribute()
            { ExceptionType = typeof(HttpAntiForgeryException) }
            );
        }
    }
}