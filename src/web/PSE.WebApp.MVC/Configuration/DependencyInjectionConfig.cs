using System;
using Microsoft.Extensions.DependencyInjection;
using PSE.WebApp.MVC.Services;

namespace PSE.WebApp.MVC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddHttpClient<IAuthenticateService, AuthenticateService>();
        }
    }
}
