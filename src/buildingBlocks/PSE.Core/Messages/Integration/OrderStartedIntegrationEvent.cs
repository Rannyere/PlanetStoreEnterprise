using System;
namespace PSE.Core.Messages.Integration
{
    public class OrderStartedIntegrationEvent : IntegrationEvent
    {
        public Guid CustomerId { get; set; }
        public Guid OrderId { get; set; }
        public int PaymentMethod { get; set; }
        public decimal TotalValue { get; set; }

        public string CardNumber { get; set; }
        public string CardHolder { get; set; }
        public string CardExpiration { get; set; }
        public string CardCvv { get; set; }
    }
}
