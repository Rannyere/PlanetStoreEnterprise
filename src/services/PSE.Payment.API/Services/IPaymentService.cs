using System;
using System.Threading.Tasks;
using PSE.Core.Messages.Integration;
using PSE.Payment.API.Models;

namespace PSE.Payment.API.Services
{
    public interface IPaymentService
    {
        Task<ResponseMessage> AuthorizePayment(PaymentInfo payment);
    }
}
