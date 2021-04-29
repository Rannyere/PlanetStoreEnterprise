using System;
using PSE.Core.Data;

namespace PSE.Payment.API.Models
{
    public interface IPaymentRepository : IRepository<PaymentInfo>
    {
        void AddPayment(PaymentInfo payment);
    }
}
