using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using PSE.Core.Mediator;
using PSE.Order.API.Application.Commands;
using PSE.Order.API.Application.Events;
using PSE.Order.API.Application.Queries;
using PSE.Order.Domain.Orders;
using PSE.Order.Domain.Vouchers;
using PSE.Order.Infra.Data;
using PSE.Order.Infra.Data.Repository;
using PSE.WebAPI.Core.User;

namespace PSE.Order.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        // API
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IAspNetUser, AspNetUser>();

        // Commands
        services.AddScoped<IRequestHandler<ExecuteOrderCommand, ValidationResult>, OrderCommandHandler>();

        // Events
       // services.AddScoped<INotificationHandler<OrderExecutedEvent>, OrderEventHandler>();

        // Application
        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IVoucherQueries, VoucherQueries>();
        services.AddScoped<IOrderQueries, OrderQueries>();

        // Data
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IVoucherRepository, VoucherRepository>();
        services.AddScoped<OrderDbContext>();
    }
}