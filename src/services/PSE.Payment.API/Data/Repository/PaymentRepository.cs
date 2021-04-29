using System;
using PSE.Core.Data;
using PSE.Payment.API.Models;

namespace PSE.Payment.API.Data.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentDbContext _context;

        public PaymentRepository(PaymentDbContext context)
        {
            _context = context;
        }

        public IUnityOfWork UnitOfWork => _context;

        public void AddPayment(PaymentInfo payment)
        {
            _context.Payments.Add(payment);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
