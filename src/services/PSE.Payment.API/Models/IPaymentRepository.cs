using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PSE.Core.Data;

namespace PSE.Payment.API.Models
{
    public interface IPaymentRepository : IRepository<PaymentInfo>
    {
        void AddPayment(PaymentInfo payment);
        void AddTransaction(Transaction transaction);
        Task<PaymentInfo> GetPaymentByOrderId(Guid orderId);
        Task<IEnumerable<Transaction>> GetTransactionsByOrderId(Guid orderId);
    }
}
