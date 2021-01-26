using System;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PSE.Clients.API.Application.Commands;
using PSE.Clients.API.Data;
using PSE.Core.Mediator;

namespace PSE.Clients.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ClientsDbContext>();

            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<RegisterCustomerCommand, ValidationResult>, CustomerCommandHandler>();
        }
    }
}
