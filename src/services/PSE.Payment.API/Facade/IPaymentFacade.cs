using PSE.Payment.API.Models;
using System.Threading.Tasks;

namespace PSE.Payment.API.Facade;

public interface IPaymentFacade
{
    Task<Transaction> AuthorizePayment(PaymentInfo payment);
    Task<Transaction> CancelPayment(Transaction transaction);
    Task<Transaction> CapturePayment(Transaction transaction);
}