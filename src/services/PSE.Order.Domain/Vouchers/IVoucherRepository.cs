using System;
using System.Threading.Tasks;
using PSE.Core.Data;

namespace PSE.Order.Domain.Vouchers
{
    public interface IVoucherRepository : IRepository<Voucher>
    {
        Task<Voucher> GetVoucherByCode(string code);
    }
}
