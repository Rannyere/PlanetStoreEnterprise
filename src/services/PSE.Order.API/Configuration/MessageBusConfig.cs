using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PSE.Core.Utils;
using PSE.MessageBus;
using PSE.Order.API.Services;

namespace PSE.Order.API.Configuration;

public static class MessageBusConfig
{
    public static void AddMessageBusConfiguration(this IServiceCollection services,
                                                       IConfiguration configuration)
    {
        services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
            .AddHostedService<OrderOrchestratorIntegrationHandler>()
            .AddHostedService<OrderIntegrationHandler>();
    }
}