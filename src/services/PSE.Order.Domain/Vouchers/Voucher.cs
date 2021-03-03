using System;
using PSE.Core.DomainObjects;

namespace PSE.Order.Domain.Vouchers
{
    public class Voucher : Entity, IAggregatedRoot
    {
        public string Code { get; private set; }
        public decimal? Percentage { get; private set; }
        public decimal? DiscountValue { get; private set; }
        public int Quantity { get; private set; }
        public DiscountTypeVoucher DiscountType { get; private set; }
        public DateTime DateCreation { get; private set; }
        public DateTime? DateUsage { get; private set; }
        public DateTime DateValidity { get; private set; }
        public bool Activ { get; private set; }
        public bool Usage { get; private set; }

    }
}
