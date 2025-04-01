using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PSE.Core.Utils;
using PSE.MessageBus;
using PSE.Payment.API.Services;

namespace PSE.Payment.API.Configuration;

public static class MessageBusConfig
{
    public static void AddMessageBusConfiguration(this IServiceCollection services,
                                                       IConfiguration configuration)
    {
        services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
            .AddHostedService<PaymentIntegrationHandler>();
    }
}