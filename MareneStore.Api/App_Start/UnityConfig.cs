using MareneStore.Application.Interfaces;
using MareneStore.Application.Services;
using MareneStore.Infrastructure.Data;
using MareneStore.Infrastructure.Data.Interfaces;
using MareneStore.Infrastructure.Data.Repositories;
using MareneStore.Infrastructure.Identity;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace MareneStore.Api
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IProductService, ProductService>(new HierarchicalLifetimeManager());
            container.RegisterType<IOrderService, OrderService>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserService, UserService>(new HierarchicalLifetimeManager());
            container.RegisterType<IStockService, StockService>(new HierarchicalLifetimeManager());

            container.RegisterType<AppDbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<IdentityDbContext>(new HierarchicalLifetimeManager());

            container.RegisterType<IProductRepository, ProductRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IOrderRepository, OrderRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IStockRepository, StockRepository>(new HierarchicalLifetimeManager());

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}