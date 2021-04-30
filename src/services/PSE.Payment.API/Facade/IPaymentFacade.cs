using System;
using System.Threading.Tasks;
using PSE.Payment.API.Models;

namespace PSE.Payment.API.Facade
{
    public interface IPaymentFacade
    {
        Task<Transaction> AuthorizePayment(PaymentInfo payment);
    }
}
