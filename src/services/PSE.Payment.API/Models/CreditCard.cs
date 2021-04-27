using System;
namespace PSE.Payment.API.Models
{
    public class CreditCard
    {
        public string CardHolder { get; set; }
        public string CardNumber { get; set; }
        public string CardExpiration { get; set; }
        public string CardCvv { get; set; }

        protected CreditCard() { }

        public CreditCard(string cardHolder, string cardNumber, string cardExpiration, string cardCvv)
        {
            CardHolder = cardHolder;
            CardNumber = cardNumber;
            CardExpiration = cardExpiration;
            CardCvv = cardCvv;
        }
    }
}
