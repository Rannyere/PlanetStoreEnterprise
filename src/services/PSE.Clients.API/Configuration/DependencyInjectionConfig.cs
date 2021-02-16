using System;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PSE.Clients.API.Application.Commands;
using PSE.Clients.API.Application.Events;
using PSE.Clients.API.Data;
using PSE.Clients.API.Data.Repository;
using PSE.Clients.API.Models;
using PSE.Core.Mediator;

namespace PSE.Clients.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ClientsDbContext>();

            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<CustomerRegisterCommand, ValidationResult>, CustomerCommandHandler>();

            services.AddScoped<INotificationHandler<CustomerRegisteredEvent>, CustomerEventHandler>();
        }
    }
}
