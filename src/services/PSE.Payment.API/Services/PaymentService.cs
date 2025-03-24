using FluentValidation.Results;
using PSE.Core.DomainObjects;
using PSE.Core.Messages.Integration;
using PSE.Payment.API.Facade;
using PSE.Payment.API.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PSE.Payment.API.Services;

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

    public async Task<ResponseMessage> CapturePayment(Guid orderId)
    {
        var transactions = await _paymentRepository.GetTransactionsByOrderId(orderId);
        var transactionAuthorized = transactions?.FirstOrDefault(t => t.Status == StatusTransaction.Authorized);
        var validationResult = new ValidationResult();

        if (transactionAuthorized == null) throw new DomainException($"transaction not found - order {orderId}");

        var transaction = await _paymentFacade.CapturePayment(transactionAuthorized);

        if (transaction.Status != StatusTransaction.Paid)
        {
            validationResult.Errors.Add(new ValidationFailure("Payment",
                $"Order payment could not be captured {orderId}"));

            return new ResponseMessage(validationResult);
        }

        transaction.PaymentId = transactionAuthorized.PaymentId;
        _paymentRepository.AddTransaction(transaction);

        if (!await _paymentRepository.UnitOfWork.Commit())
        {
            validationResult.Errors.Add(new ValidationFailure("Payment",
                $"Order payment capture could not be persisted {orderId}"));

            return new ResponseMessage(validationResult);
        }

        return new ResponseMessage(validationResult);
    }

    public async Task<ResponseMessage> CancelPayment(Guid orderId)
    {
        var transactions = await _paymentRepository.GetTransactionsByOrderId(orderId);
        var transactionAuthorized = transactions?.FirstOrDefault(t => t.Status == StatusTransaction.Authorized);
        var validationResult = new ValidationResult();

        if (transactionAuthorized == null) throw new DomainException($"transaction not found - order {orderId}");

        var transaction = await _paymentFacade.CancelPayment(transactionAuthorized);

        if (transaction.Status != StatusTransaction.Canceled)
        {
            validationResult.Errors.Add(new ValidationFailure("Payment",
                $"Order payment could not be canceled {orderId}"));

            return new ResponseMessage(validationResult);
        }

        transaction.PaymentId = transactionAuthorized.PaymentId;
        _paymentRepository.AddTransaction(transaction);

        if (!await _paymentRepository.UnitOfWork.Commit())
        {
            validationResult.Errors.Add(new ValidationFailure("Payment",
                $"Order payment cancel could not be persisted {orderId}"));

            return new ResponseMessage(validationResult);
        }

        return new ResponseMessage(validationResult);
    }
}