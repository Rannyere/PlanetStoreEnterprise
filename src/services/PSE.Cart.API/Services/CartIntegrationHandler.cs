using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PSE.Cart.API.Data;
using PSE.Core.Messages.Integration;
using PSE.MessageBus;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PSE.Cart.API.Services;

public class CartIntegrationHandler : BackgroundService
{
    private readonly IMessageBus _bus;
    private readonly IServiceProvider _serviceProvider;

    public CartIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider)
    {
        _bus = bus;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _bus.SubscribeAsync<OrderExecutedIntegrationEvent>("OrderExecuted", DeleteCart);
    }

    private async Task DeleteCart(OrderExecutedIntegrationEvent message)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CartDbContext>();

        var cart = await context.CartCustomers
            .FirstOrDefaultAsync(c => c.CustomerId == message.CustomerId);

        if (cart != null)
        {
            context.CartCustomers.Remove(cart);
            await context.SaveChangesAsync();
        }
    }
}
