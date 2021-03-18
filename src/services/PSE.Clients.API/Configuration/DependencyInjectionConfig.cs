using System;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PSE.Clients.API.Application.Commands;
using PSE.Clients.API.Application.Events;
using PSE.Clients.API.Data;
using PSE.Clients.API.Data.Repository;
using PSE.Clients.API.Models;
using PSE.Core.Mediator;
using PSE.WebAPI.Core.User;

namespace PSE.Clients.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Application
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();

            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Command
            services.AddScoped<IRequestHandler<CustomerRegisterCommand, ValidationResult>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<AddAddressCommand, ValidationResult>, CustomerCommandHandler>();

            // Event
            services.AddScoped<INotificationHandler<CustomerRegisteredEvent>, CustomerEventHandler>();

            // Data
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ClientsDbContext>();
        }
    }
}
