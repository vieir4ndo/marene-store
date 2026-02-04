﻿using MareneStore.Api.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace MareneStore.Api
{
    public static class Registrations
    {
        public static IServiceCollection AddControllers(this IServiceCollection services)
        {
            services.AddTransient<ProductController>();
            services.AddTransient<OrderController>();

            return services;
        }
    }
}