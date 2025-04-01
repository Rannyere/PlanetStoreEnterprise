using PSE.Core.DomainObjects;
using PSE.Order.Domain.Vouchers.Specs;
using System;

namespace PSE.Order.Domain.Vouchers;

public class Voucher : Entity, IAggregatedRoot
{
    public string Code { get; private set; }
    public decimal? DiscountPercentage { get; private set; }
    public decimal? DiscountValue { get; private set; }
    public int Quantity { get; private set; }
    public DiscountTypeVoucher DiscountType { get; private set; }
    public DateTime DateCreation { get; private set; }
    public DateTime? DateUsage { get; private set; }
    public DateTime DateValidity { get; private set; }
    public bool Activ { get; private set; }
    public bool Usage { get; private set; }

    public bool IsValidForUse()
    {
        return new VoucherActivSpecification()
            .And(new VoucherDateSpecification())
            .And(new VoucherQuantitySpecification())
            .IsSatisfiedBy(this);
    }

    public void MarkAsUsage()
    {
        Activ = false;
        Usage = true;
        Quantity = 0;
        DateUsage = DateTime.Now;
    }

    public void DebitStock()
    {
        Quantity -= 1;
        if (Quantity >= 1) return;

        MarkAsUsage();
    }
}