using PSE.Order.API.Application.DTOs;
using PSE.Order.Domain.Vouchers;
using System.Threading.Tasks;

namespace PSE.Order.API.Application.Queries;

public interface IVoucherQueries
{
    Task<VoucherDTO> GetVoucherByCode(string code);
}

public class VoucherQueries : IVoucherQueries
{
    private readonly IVoucherRepository _voucherRepository;

    public VoucherQueries(IVoucherRepository voucherRepository)
    {
        _voucherRepository = voucherRepository;
    }

    public async Task<VoucherDTO> GetVoucherByCode(string code)
    {
        var voucher = await _voucherRepository.GetVoucherByCode(code);

        if (voucher == null) return null;

        //Validations Voucher
        if (!voucher.IsValidForUse()) return null;

        return new VoucherDTO
        {
            Code = voucher.Code,
            DiscountType = (int)voucher.DiscountType,
            DiscountPercentage = voucher.DiscountPercentage,
            DiscountValue = voucher.DiscountValue
        };
    }
}