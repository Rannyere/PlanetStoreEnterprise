using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using PSE.WebAPI.Core.Extensions;
using PSE.WebAPI.Core.User;
using PSE.WebApp.MVC.Extensions;
using PSE.WebApp.MVC.Services;
using PSE.WebApp.MVC.Services.Handlers;
using PSE.WebApp.MVC.Services.Interfaces;
using System;

namespace PSE.WebApp.MVC.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IValidationAttributeAdapterProvider, CpfValidationAttributeAdapterProvider>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IAspNetUser, AspNetUser>();

        #region httpServices

        services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

        services.AddHttpClient<IAuthenticateService, AuthenticateService>()
            .AddPolicyHandler(PollyExtensions.WaitAndRetry())
            .AllowSelfSignedCertificate()
            .AddTransientHttpErrorPolicy(
                p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

        services.AddHttpClient<ICatalogService, CatalogService>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            //.AddTransientHttpErrorPolicy(
            //p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)));
            .AddPolicyHandler(PollyExtensions.WaitAndRetry())
            .AllowSelfSignedCertificate()
            .AddTransientHttpErrorPolicy(
                p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

        services.AddHttpClient<ICustomerService, CustomerService>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            .AddPolicyHandler(PollyExtensions.WaitAndRetry())
            .AllowSelfSignedCertificate()
            .AddTransientHttpErrorPolicy(
                p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

        services.AddHttpClient<ISalesBffService, SalesBffService>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
            .AddPolicyHandler(PollyExtensions.WaitAndRetry())
            .AllowSelfSignedCertificate()
            .AddTransientHttpErrorPolicy(
                p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

        #endregion

        #region Refit
        //services.AddHttpClient("Refit", options =>
        //{
        //    options.BaseAddress = new Uri(configuration.GetSection("CatalogUrl").Value);
        //})
        //    .AddTypedClient(Refit.RestService.For<ICatalogServiceRefit>)
        //    .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();
        #endregion
    }
}
