using MareneStore.Api.App_Start;
using System.Web.Http;

namespace MareneStore.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new ExceptionMiddleware());
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "MareneStoreApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new
                {
                    id = RouteParameter.Optional
                }
            );
        }
    }
}

