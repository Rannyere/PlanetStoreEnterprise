using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PSE.Core.Messages.Integration;
using PSE.MessageBus;
using PSE.Order.API.Application.Queries;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PSE.Order.API.Services;

public class OrderOrchestratorIntegrationHandler : IHostedService, IDisposable
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OrderOrchestratorIntegrationHandler> _logger;
    private Timer _timer;
    private bool _isProcessing;

    public OrderOrchestratorIntegrationHandler(IServiceProvider serviceProvider,
                                               ILogger<OrderOrchestratorIntegrationHandler> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Order processing service started");

        _timer = new Timer(ProcessOrders, null, TimeSpan.Zero, TimeSpan.FromSeconds(15));

        return Task.CompletedTask;
    }

    private async void ProcessOrders(object state)
    {
        if (_isProcessing) return;

        try
        {
            _isProcessing = true;
            _logger.LogInformation("Order processing service in progress");

            using (var scope = _serviceProvider.CreateScope())
            {
                var orderQueries = scope.ServiceProvider.GetRequiredService<IOrderQueries>();
                var order = await orderQueries.GetOrdersAuthorized();

                if (order == null) return;

                var bus = scope.ServiceProvider.GetRequiredService<IMessageBus>();

                var orderAuthorized = new OrderAuthorizedIntegrationEvent(order.CustomerId, order.Id,
                    order.OrderItems.ToDictionary(p => p.ProductId, p => p.Quantity));

                await bus.PublishAsync(orderAuthorized);

                _logger.LogInformation($"Order ID: {order.Id} forwarded to CatalogAPI to manage inventory - subtract items from stock.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing orders");
        }
        finally
        {
            _isProcessing = false;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Order processing service closed");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}