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

            services
               .AddControllers()
               .AddApplication()
               .AddInfrastructureData();

            var serviceProvider = services.BuildServiceProvider();

            config.DependencyResolver = new DefaultDependencyResolver(serviceProvider);

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                  name: "MareneStoreApi",
                routeTemplate: "api/v{version}/{controller}/{id}",
                defaults: new
                {
                    version = "1",
                    id = RouteParameter.Optional
                }
            );
        }
    }
}

