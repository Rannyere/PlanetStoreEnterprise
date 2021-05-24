using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PSE.Cart.API.Services.gRPC;
using PSE.Sales.BFF.Services.gRPC;

namespace PSE.Sales.BFF.Configurations
{
    public static class GrpcConfig
    {
        public static void ConfigureGrpcServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<GrpcServiceInterceptor>();

            services.AddScoped<ICartGrpcService, CartGrpcService>();

            services.AddGrpcClient<CartGrpcIntegration.CartGrpcIntegrationClient>(options =>
            {
                options.Address = new Uri(configuration["CartUrl"]);
            }).AddInterceptor<GrpcServiceInterceptor>();
        }
    }
}
