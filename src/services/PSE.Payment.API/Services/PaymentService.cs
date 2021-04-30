using System;
using System.Threading.Tasks;
using FluentValidation.Results;
using PSE.Core.Messages.Integration;
using PSE.Payment.API.Facade;
using PSE.Payment.API.Models;

namespace PSE.Payment.API.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentFacade _paymentFacade;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentFacade paymentFacade, IPaymentRepository paymentRepository)
        {
            _paymentFacade = paymentFacade;
            _paymentRepository = paymentRepository;
        }

        public async Task<ResponseMessage> AuthorizePayment(PaymentInfo payment)
        {
            var transaction = await _paymentFacade.AuthorizePayment(payment);
            var validationResult = new ValidationResult();

            if (transaction.Status != StatusTransaction.Authorized)
            {
                validationResult.Errors.Add(new ValidationFailure("Payment",
                        "Payment declined, contact your card operator"));

                return new ResponseMessage(validationResult);
            }

            payment.AddTransaction(transaction);
            _paymentRepository.AddPayment(payment);

            if (!await _paymentRepository.UnitOfWork.Commit())
            {
                validationResult.Errors.Add(new ValidationFailure("Payment",
                    "There was an error making the payment."));

                return new ResponseMessage(validationResult);
            }

            return new ResponseMessage(validationResult);
        }
    }
}
