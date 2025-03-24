using PSE.Core.Specification;
using System;
using System.Linq.Expressions;

namespace PSE.Order.Domain.Vouchers.Specs;

public class VoucherDateSpecification : Specification<Voucher>
{
    public override Expression<Func<Voucher, bool>> ToExpression()
    {
        return voucher => voucher.DateValidity >= DateTime.Now;
    }
}

public class VoucherQuantitySpecification : Specification<Voucher>
{
    public override Expression<Func<Voucher, bool>> ToExpression()
    {
        return voucher => voucher.Quantity > 0;
    }
}

public class VoucherActivSpecification : Specification<Voucher>
{
    public override Expression<Func<Voucher, bool>> ToExpression()
    {
        return voucher => voucher.Activ && !voucher.Usage;
    }
}