using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PSE.Core.DomainObjects;
using PSE.Core.Messages.Integration;
using PSE.MessageBus;
using PSE.Payment.API.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PSE.Payment.API.Services;

public class PaymentIntegrationHandler : BackgroundService
{
    private readonly IMessageBus _bus;
    private readonly IServiceProvider _serviceProvider;

    public PaymentIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider)
    {
        _bus = bus;
        _serviceProvider = serviceProvider;
    }

    private void SetResponder()
    {
        _bus.RespondAsync<OrderStartedIntegrationEvent, ResponseMessage>(async request =>
            await AuthorizePayment(request));
    }

    private void SetSubscribers()
    {
        _bus.SubscribeAsync<OrderCanceledIntegrationEvent>("OrderCanceled", async request =>
            await CancelPayment(request));

        _bus.SubscribeAsync<OrderWritedOffStockIntegrationEvent>("OrderWriteOff", async request =>
            await CapturePayment(request));
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        SetResponder();
        SetSubscribers();
        return Task.CompletedTask;
    }

    private async Task<ResponseMessage> AuthorizePayment(OrderStartedIntegrationEvent message)
    {
        using var scope = _serviceProvider.CreateScope();
        var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();
        var payment = new PaymentInfo
        {
            OrderId = message.OrderId,
            PaymentMehtod = (PaymentMehtod)message.PaymentMethod,
            TotalValue = message.TotalValue,
            CreditCard = new CreditCard(
                message.CardHolder, message.CardNumber, message.CardExpiration, message.CardCvv)
        };

        var response = await paymentService.AuthorizePayment(payment);

        return response;
    }

    private async Task CapturePayment(OrderWritedOffStockIntegrationEvent message)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();

            var response = await paymentService.CapturePayment(message.OrderId);

            if (!response.ValidationResult.IsValid)
                throw new DomainException($"Failed to capture order payment {message.OrderId}");

            await _bus.PublishAsync(new OrderPaidIntegrationEvent(message.CustomerId, message.OrderId));
        }
    }

    private async Task CancelPayment(OrderCanceledIntegrationEvent message)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();

            var response = await paymentService.CancelPayment(message.OrderId);

            if (!response.ValidationResult.IsValid)
                throw new DomainException($"Failed to cancel order payment {message.OrderId}");
        }
    }
}