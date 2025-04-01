using PSE.Core.Data;
using System.Threading.Tasks;

namespace PSE.Order.Domain.Vouchers;

public interface IVoucherRepository : IRepository<Voucher>
{
    Task<Voucher> GetVoucherByCode(string code);

    void Update(Voucher voucher);
}