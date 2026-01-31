using MareneStore.Infrastructure.Data.Interfaces;
using MareneStore.Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MareneStore.Infrastructure.Data
{
    public static class Registrations
    {
        public static IServiceCollection AddInfrastructureData(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }

    }
}
