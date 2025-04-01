using Microsoft.EntityFrameworkCore;
using PSE.Core.Data;
using PSE.Payment.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSE.Payment.API.Data.Repository;

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

    public void AddTransaction(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
    }

    public async Task<PaymentInfo> GetPaymentByOrderId(Guid orderId)
    {
        return await _context.Payments.AsNoTracking()
            .FirstOrDefaultAsync(p => p.OrderId == orderId);
    }

    public async Task<IEnumerable<Transaction>> GetTransactionsByOrderId(Guid orderId)
    {
        return await _context.Transactions.AsNoTracking()
            .Where(t => t.Payment.OrderId == orderId).ToListAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}