using PSE.Core.DomainObjects;
using System;

namespace PSE.Order.Domain.Orders;

public class OrderItem : Entity
{
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public string Name { get; private set; }
    public int Quantity { get; private set; }
    public decimal Value { get; private set; }
    public string Image { get; set; }

    // EF Rel.
    public OrderCustomer OrderCustomer { get; set; }

    public OrderItem(Guid productId, string name, int quantity, decimal value, string image)
    {
        ProductId = productId;
        Name = name;
        Quantity = quantity;
        Value = value;
        Image = image;
    }

    // EF ctor
    protected OrderItem() { }

    internal decimal CalculateValue()
    {
        return Quantity * Value;
    }
}
