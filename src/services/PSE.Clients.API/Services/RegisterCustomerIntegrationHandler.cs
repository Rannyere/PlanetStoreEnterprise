using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PSE.Clients.API.Application.Commands;
using PSE.Core.Mediator;
using PSE.Core.Messages.Integration;

namespace PSE.Clients.API.Services
{
    public class RegisterCustomerIntegrationHandler : BackgroundService
    {
        private IBus _bus;
        private IServiceProvider _serviceProvider;

        public RegisterCustomerIntegrationHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus = RabbitHutch.CreateBus("host=localhost:5672");

            _bus.Rpc.RespondAsync<UserRegisteredIntegrationEvent, ResponseMessage>(async request =>
               new ResponseMessage(await RegisterCustomer(request)));

            return Task.CompletedTask;
        }

        private async Task<ValidationResult> RegisterCustomer(UserRegisteredIntegrationEvent message)
        {
            var customerCommand = new CustomerRegisterCommand(message.Id, message.Name, message.Email, message.Cpf);
            ValidationResult success;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
                success = await mediator.SendCommand(customerCommand);
            }

            return success;
        }
    }
}
