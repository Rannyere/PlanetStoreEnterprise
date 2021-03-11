using System;
using PSE.Core.Specification.Validation;

namespace PSE.Order.Domain.Vouchers.Specs
{
    public class VoucherValidation : SpecValidator<Voucher>
    {
        public VoucherValidation()
        {
            var dateSpec = new VoucherDateSpecification();
            var quantitySpec = new VoucherQuantitySpecification();
            var activSpec = new VoucherActivSpecification();

            Add("dateSpec", new Rule<Voucher>(dateSpec, "This voucher is expired"));
            Add("quantitySpec", new Rule<Voucher>(quantitySpec, "This voucher has already been used"));
            Add("activSpec", new Rule<Voucher>(activSpec, "This voucher is no longer active"));
        }
    }
}
