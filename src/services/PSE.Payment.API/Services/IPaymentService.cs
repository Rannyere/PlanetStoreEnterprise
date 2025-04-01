using PSE.Core.Messages.Integration;
using PSE.Payment.API.Models;
using System;
using System.Threading.Tasks;

namespace PSE.Payment.API.Services;

public interface IPaymentService
{
    Task<ResponseMessage> AuthorizePayment(PaymentInfo payment);
    Task<ResponseMessage> CancelPayment(Guid orderId);
    Task<ResponseMessage> CapturePayment(Guid orderId);
}