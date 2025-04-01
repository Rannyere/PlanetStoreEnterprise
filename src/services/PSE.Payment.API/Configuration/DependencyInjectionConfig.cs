using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PSE.Payment.API.Data;
using PSE.Payment.API.Data.Repository;
using PSE.Payment.API.Facade;
using PSE.Payment.API.Models;
using PSE.Payment.API.Services;
using PSE.WebAPI.Core.User;

namespace PSE.Payment.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IAspNetUser, AspNetUser>();

        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IPaymentFacade, PaymentCreditCardFacade>();

        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<PaymentDbContext>();
    }
}