using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PSE.Catalog.API.Models;
using PSE.Core.DomainObjects;
using PSE.Core.Messages.Integration;
using PSE.MessageBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PSE.Catalog.API.Services;

public class CatalogIntegrationHandler : BackgroundService
{
    private readonly IMessageBus _bus;
    private readonly IServiceProvider _serviceProvider;

    public CatalogIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider)
    {
        _bus = bus;
        _serviceProvider = serviceProvider;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        SetSubscribers();
        return Task.CompletedTask;
    }

    private void SetSubscribers()
    {
        _bus.SubscribeAsync<OrderAuthorizedIntegrationEvent>("OrderAuthorized", async request =>
            await ManageInventory(request));
    }

    private async Task ManageInventory(OrderAuthorizedIntegrationEvent message)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var productsWithStock = new List<Product>();
            var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();

            var idsProducts = string.Join(",", message.Items.Select(c => c.Key));
            var products = await productRepository.GetPoductsById(idsProducts);

            if (products.Count != message.Items.Count)
            {
                CancelOrderWithOutStock(message);
                return;
            }

            foreach (var product in products)
            {
                var quantityProduct = message.Items.FirstOrDefault(p => p.Key == product.Id).Value;

                if (product.IsAvailable(quantityProduct))
                {
                    product.RemoveStock(quantityProduct);
                    productsWithStock.Add(product);
                }
            }

            if (productsWithStock.Count != message.Items.Count)
            {
                CancelOrderWithOutStock(message);
                return;
            }

            foreach (var product in productsWithStock)
            {
                productRepository.Update(product);
            }

            if (!await productRepository.UnitOfWork.Commit())
            {
                throw new DomainException($"Failed to update inventory - {message.OrderId}");
            }

            var orderWriteOfftStock = new OrderWritedOffStockIntegrationEvent(message.CustomerId, message.OrderId);
            await _bus.PublishAsync(orderWriteOfftStock);
        }
    }

    public async void CancelOrderWithOutStock(OrderAuthorizedIntegrationEvent message)
    {
        var orderCanceled = new OrderCanceledIntegrationEvent(message.CustomerId, message.OrderId);
        await _bus.PublishAsync(orderCanceled);
    }
}