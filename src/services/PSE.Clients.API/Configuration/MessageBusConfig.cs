using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PSE.Clients.API.Services;
using PSE.Core.Utils;
using PSE.MessageBus;

namespace PSE.Clients.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
                                                           IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<RegisterCustomerIntegrationHandler>(); 
        }
    }
}
