using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PSE.Clients.API.Application.Commands;
using PSE.Core.Mediator;
using PSE.Core.Messages.Integration;
using PSE.MessageBus;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PSE.Clients.API.Services;

public class RegisterCustomerIntegrationHandler : BackgroundService
{
    private readonly IMessageBus _bus;
    private readonly IServiceProvider _serviceProvider;

    public RegisterCustomerIntegrationHandler(IServiceProvider serviceProvider,
                                              IMessageBus bus)
    {
        _serviceProvider = serviceProvider;
        _bus = bus;
    }

    private void SetResponder()
    {
        _bus.RespondAsync<UserRegisteredIntegrationEvent, ResponseMessage>(async request =>
            await RegisterCustomer(request));
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        SetResponder();
        return Task.CompletedTask;
    }

    private async Task<ResponseMessage> RegisterCustomer(UserRegisteredIntegrationEvent message)
    {
        var customerCommand = new CustomerRegisterCommand(message.Id, message.Name, message.Email, message.Cpf);
        ValidationResult success;

        using (var scope = _serviceProvider.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            success = await mediator.SendCommand(customerCommand);
        }

        return new ResponseMessage(success);
    }
}