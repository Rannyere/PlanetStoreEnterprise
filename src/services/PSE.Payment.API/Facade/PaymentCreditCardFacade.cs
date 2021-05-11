using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PSE.Payment.API.Models;
using PSE.Payment.Gateway;

namespace PSE.Payment.API.Facade
{
    public class PaymentCreditCardFacade : IPaymentFacade
    {
        private readonly PaymentConfig _paymentConfig;

        public PaymentCreditCardFacade(IOptions<PaymentConfig> paymentConfig)
        {
            _paymentConfig = paymentConfig.Value;
        }

        public async Task<Transaction> AuthorizePayment(PaymentInfo payment)
        {
            var planetPaySvc = new PlanetPayService(_paymentConfig.DefaultApiKey,
                _paymentConfig.DefaultEncryptionKey);

            var cardHashGen = new CardHash(planetPaySvc)
            {
                CardNumber = payment.CreditCard.CardNumber,
                CardHolderName = payment.CreditCard.CardHolder,
                CardExpirationDate = payment.CreditCard.CardExpiration,
                CardCvv = payment.CreditCard.CardCvv
            };
            var cardHash = cardHashGen.Generate();

            var dealTransaction = new DealTransaction(planetPaySvc)
            {
                CardHash = cardHash,
                CardNumber = payment.CreditCard.CardNumber,
                CardHolderName = payment.CreditCard.CardHolder,
                CardExpirationDate = payment.CreditCard.CardExpiration,
                CardCvv = payment.CreditCard.CardCvv,
                PaymentMethod = PaymentMethod.CreditCard,
                Amount = payment.TotalValue
            };

            return ConvertToTransaction(await dealTransaction.AuthorizeCardTransaction());
        }

        public async Task<Transaction> CapturePayment(Transaction transaction)
        {
            var planetPaySvc = new PlanetPayService(_paymentConfig.DefaultApiKey,
                _paymentConfig.DefaultEncryptionKey);

            var dealTransaction = ConvertToDealTransaction(transaction, planetPaySvc);

            return ConvertToTransaction(await dealTransaction.CaptureCardTransaction());
        }

        public async Task<Transaction> CancelPayment(Transaction transaction)
        {
            var planetPaySvc = new PlanetPayService(_paymentConfig.DefaultApiKey,
                _paymentConfig.DefaultEncryptionKey);

            var dealTransaction = ConvertToDealTransaction(transaction, planetPaySvc);

            return ConvertToTransaction(await dealTransaction.CancelAuthorization());
        }

        public static Transaction ConvertToTransaction(DealTransaction dealTransaction)
        {
            return new Transaction
            {
                PaymentId = Guid.NewGuid(),
                Status = (StatusTransaction)dealTransaction.Status,
                TotalValue = dealTransaction.Amount,
                FlagCard = dealTransaction.CardBrand,
                AuthorizationCode = dealTransaction.AuthorizationCode,
                CostTransaction = dealTransaction.Cost,
                DateTransaction = dealTransaction.TransactionDate,
                NSU = dealTransaction.Nsu,
                TID = dealTransaction.Tid
            };
        }

        public static DealTransaction ConvertToDealTransaction(Transaction transaction, PlanetPayService planetPayService)
        {
            return new DealTransaction(planetPayService)
            {
                Status = (TransactionStatus)transaction.Status,
                Amount = transaction.TotalValue,
                CardBrand = transaction.FlagCard,
                AuthorizationCode = transaction.AuthorizationCode,
                Cost = transaction.CostTransaction,
                Nsu = transaction.NSU,
                Tid = transaction.TID
            };
        }
    }
}
