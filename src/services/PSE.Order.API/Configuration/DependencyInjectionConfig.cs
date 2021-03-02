using Microsoft.Extensions.DependencyInjection;
using PSE.Order.Infra.Data;

namespace PSE.Order.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<OrderDbContext>();
        }
    }
}
