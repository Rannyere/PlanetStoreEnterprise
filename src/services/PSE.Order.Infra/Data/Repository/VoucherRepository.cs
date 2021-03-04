using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PSE.Core.Data;
using PSE.Order.Domain.Vouchers;

namespace PSE.Order.Infra.Data.Repository
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly OrderDbContext _context;

        public VoucherRepository(OrderDbContext context)
        {
            _context = context;
        }

        public IUnityOfWork UnitOfWork => _context;

        public async Task<Voucher> GetVoucherByCode(string code)
        {
            return await _context.Vouchers.FirstOrDefaultAsync(p => p.Code == code);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
