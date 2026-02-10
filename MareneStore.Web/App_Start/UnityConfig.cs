using MareneStore.Application.Interfaces;
using MareneStore.Application.Services;
using MareneStore.Infrastructure.Data;
using MareneStore.Infrastructure.Data.Interfaces;
using MareneStore.Infrastructure.Data.Repositories;
using MareneStore.Infrastructure.Identity;
using MareneStore.Web.Managers;
using Microsoft.AspNet.Identity.Owin;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.Lifetime;
using Unity.Mvc5;

namespace MareneStore.Web
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

            container.RegisterFactory<ApplicationUserManager>(c =>
            {
                return HttpContext.Current
                    .GetOwinContext()
                    .GetUserManager<ApplicationUserManager>();
            });

            container.RegisterFactory<ApplicationSignInManager>(c =>
            {
                return HttpContext.Current
                    .GetOwinContext()
                    .Get<ApplicationSignInManager>();
            });


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}