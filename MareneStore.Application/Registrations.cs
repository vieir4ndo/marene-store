using MareneStore.Application.Interfaces;
using MareneStore.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MareneStore.Application
{
    public static class Registrations
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}
