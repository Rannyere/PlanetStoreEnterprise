using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PSE.Core.Messages.Integration;
using PSE.MessageBus;
using PSE.Payment.API.Models;

namespace PSE.Payment.API.Services
{
    public class PaymentIntegrationHandler :  BackgroundService
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

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
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
    }
}
