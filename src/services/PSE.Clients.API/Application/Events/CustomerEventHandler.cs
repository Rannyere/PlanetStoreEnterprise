using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace PSE.Clients.API.Application.Events
{
    public class CustomerEventHandler : INotificationHandler<CustomerRegisteredEvent>
    {
        public Task Handle(CustomerRegisteredEvent notification, CancellationToken cancellationToken)
        {
            //Send event confirmation
            return Task.CompletedTask;
        }
    }
}
