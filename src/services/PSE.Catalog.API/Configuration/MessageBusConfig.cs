using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PSE.Core.Utils;

namespace PSE.Catalog.API.Configuration
{
    public class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
                                                           IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<CatalogIntegrationHandler>();
        }
    }
}
