using System;
using PSE.Order.Domain.Orders;

namespace PSE.Order.API.Application.DTOs
{
    public class OrderItemDTO
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal ValueUnit { get; set; }
        public string ProductImage { get; set; }

        public static OrderItem ToOrderItem(OrderItemDTO orderItemDTO)
        {
            return new OrderItem(orderItemDTO.ProductId, orderItemDTO.ProductName, orderItemDTO.Quantity,
                orderItemDTO.ValueUnit, orderItemDTO.ProductImage);
        }
    }
}
