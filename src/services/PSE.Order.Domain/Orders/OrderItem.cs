using System;
using PSE.Core.DomainObjects;

namespace PSE.Order.Domain.Orders
{
    public class OrderItem : Entity
    {
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal ValueUnit { get; private set; }
        public string ProductImage { get; set; }

        // EF Rel.
        public OrderCustomer OrderCustomer { get; set; }

        public OrderItem(Guid productId, string productName, int quantity, decimal valueUnit, string productImage)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            ValueUnit = valueUnit;
            ProductImage = productImage;
        }

        // EF ctor
        protected OrderItem() { }

        internal decimal CalculateValue()
        {
            return Quantity * ValueUnit;
        }
    }
}
