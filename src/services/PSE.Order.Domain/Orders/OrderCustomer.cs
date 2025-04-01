using PSE.Core.DomainObjects;
using PSE.Order.Domain.Vouchers;
using System;
using System.Collections.Generic;

namespace PSE.Order.Domain.Orders;

public class OrderCustomer : Entity, IAggregatedRoot
{
    public int Code { get; private set; }
    public Guid CustomerId { get; private set; }
    public Guid? VoucherId { get; private set; }
    public bool VoucherUsage { get; private set; }
    public decimal Discount { get; private set; }
    public decimal TotalValue { get; private set; }
    public DateTime DateRegister { get; private set; }
    public OrderStatus OrderStatus { get; private set; }

    private readonly List<OrderItem> _orderItems;
    public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

    public Address Address { get; private set; }

    // EF Rel.
    public Voucher Voucher { get; private set; }

    // EF ctor
    protected OrderCustomer() { }

    public OrderCustomer(Guid customerId, decimal totalValue, List<OrderItem> orderItems,
                 bool voucherUsage = false, decimal discount = 0, Guid? voucherId = null)
    {
        CustomerId = customerId;
        TotalValue = totalValue;
        _orderItems = orderItems;

        VoucherUsage = voucherUsage;
        Discount = discount;
        VoucherId = voucherId;
    }

    public void AuthorizeOrder()
    {
        OrderStatus = OrderStatus.Authorized;
    }

    public void CancelOrder()
    {
        OrderStatus = OrderStatus.Canceled;
    }

    public void FinalizeOrder()
    {
        OrderStatus = OrderStatus.Paid;
    }

    public void AssociateVoucher(Voucher voucher)
    {
        VoucherUsage = true;
        VoucherId = voucher.Id;
        Voucher = voucher;
    }

    public void AssociateAddress(Address address)
    {
        Address = address;
    }

    public void CalculateTotalValueOrder()
    {
        if (!VoucherUsage) return;

        decimal discount = 0;
        var subtotal = TotalValue + Discount;

        if (Voucher.DiscountType == DiscountTypeVoucher.Percentage)
        {
            if (Voucher.DiscountPercentage.HasValue)
            {
                discount = (subtotal * Voucher.DiscountPercentage.Value) / 100;
                subtotal -= discount;
            }
        }
        else
        {
            if (Voucher.DiscountValue.HasValue)
            {
                discount = Voucher.DiscountValue.Value;
                subtotal -= discount;
            }
        }

        TotalValue = subtotal < 0 ? 0 : subtotal;
        Discount = discount;
    }
}