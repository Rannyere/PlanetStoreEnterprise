using System;
using Microsoft.Extensions.DependencyInjection;
using PSE.Cart.API.Data;

namespace PSE.Cart.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<CartDbContext>();
        }
    }
}
