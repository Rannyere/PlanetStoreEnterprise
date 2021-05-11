using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PSE.Core.DomainObjects;
using PSE.Core.Messages.Integration;
using PSE.MessageBus;
using PSE.Order.Domain.Orders;

namespace PSE.Order.API.Services
{
    public class OrderIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public OrderIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider)
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
            _bus.SubscribeAsync<OrderCanceledIntegrationEvent>("OrderCanceled",
                async request => await CancelOrder(request));

            _bus.SubscribeAsync<OrderPaidIntegrationEvent>("OrderPaid",
               async request => await FinalizeOrder(request));
        }

        private async Task CancelOrder(OrderCanceledIntegrationEvent message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();

                var order = await orderRepository.GetById(message.OrderId);
                order.CancelOrder();

                orderRepository.Update(order);

                if (!await orderRepository.UnitOfWork.Commit())
                {
                    throw new DomainException($"Problems canceling order {message.OrderId}");
                }
            }
        }

        private async Task FinalizeOrder(OrderPaidIntegrationEvent message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();

                var order = await orderRepository.GetById(message.OrderId);
                order.FinalizeOrder();

                orderRepository.Update(order);

                if (!await orderRepository.UnitOfWork.Commit())
                {
                    throw new DomainException($"Problems when finalizing the order {message.OrderId}");
                }
            }
        }
    }
}
