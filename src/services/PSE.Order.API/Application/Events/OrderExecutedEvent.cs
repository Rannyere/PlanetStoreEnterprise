using PSE.Core.Messages;
using System;

namespace PSE.Order.API.Application.Events;

public class OrderExecutedEvent : Event
{
    public Guid OrderId { get; private set; }
    public Guid CustomerId { get; private set; }

    public OrderExecutedEvent(Guid orderId, Guid customerId)
    {
        OrderId = orderId;
        CustomerId = customerId;
    }
}