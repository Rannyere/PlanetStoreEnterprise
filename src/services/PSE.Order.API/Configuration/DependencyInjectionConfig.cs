using Microsoft.Extensions.DependencyInjection;
using PSE.Core.Mediator;
using PSE.Order.Infra.Data;

namespace PSE.Order.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<OrderDbContext>();

            services.AddScoped<IMediatorHandler, MediatorHandler>();
        }
    }
}
