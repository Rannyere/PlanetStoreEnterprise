using System;
namespace PSE.Core.Messages.Integration;

public class OrderExecutedIntegrationEvent : IntegrationEvent
{
    public Guid CustomerId { get; private set; }

    public OrderExecutedIntegrationEvent(Guid customerId)
    {
        CustomerId = customerId;
    }
}