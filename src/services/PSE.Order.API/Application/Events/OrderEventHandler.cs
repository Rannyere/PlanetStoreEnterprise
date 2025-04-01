using MediatR;
using PSE.Core.Messages.Integration;
using PSE.MessageBus;
using System.Threading;
using System.Threading.Tasks;

namespace PSE.Order.API.Application.Events;

public class OrderEventHandler : INotificationHandler<OrderExecutedEvent>
{
    private readonly IMessageBus _messageBus;

    public OrderEventHandler(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    public async Task Handle(OrderExecutedEvent message, CancellationToken cancellationToken)
    {
        await _messageBus.PublishAsync(new OrderExecutedIntegrationEvent(message.CustomerId));
    }
}