using System.Web.Http;
using System.Web.Http.Cors;

namespace Dernek
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // New code
            var cors = new EnableCorsAttribute("http://localhost:54567/api/values", "*", "*");
            config.EnableCors();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}