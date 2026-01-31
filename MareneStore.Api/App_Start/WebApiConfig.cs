using MareneStore.Application;
using MareneStore.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Web.Http;

namespace MareneStore.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var services = new ServiceCollection();

            services.AddApplication()
                .AddInfrastructureData();

            var serviceProvider = services.BuildServiceProvider();

            config.DependencyResolver = new DefaultDependencyResolver(serviceProvider);

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

