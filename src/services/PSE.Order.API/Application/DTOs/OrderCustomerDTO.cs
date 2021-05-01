using System;
using System.Collections.Generic;
using PSE.Order.Domain.Orders;

namespace PSE.Order.API.Application.DTOs
{
    public class OrderCustomerDTO
    {
        public Guid Id { get; set; }
        public int Code { get; set; }

        public Guid CustomerId { get; set; }
        public int OrderStatus { get; set; }
        public DateTime DateRegister { get; set; }
        public decimal TotalValue { get; set; }

        public string VoucherCode { get; set; }
        public bool VoucherUsage { get; set; }
        public decimal Discount { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; }
        public AddressDTO Address { get; set; }

        public static OrderCustomerDTO ToOrderCustomerDTO(OrderCustomer orderCustomer)
        {
            var orderCustomerDTO = new OrderCustomerDTO
            {
                Id = orderCustomer.Id,
                Code = orderCustomer.Code,
                OrderStatus = (int)orderCustomer.OrderStatus,
                DateRegister = orderCustomer.DateRegister,
                TotalValue = orderCustomer.TotalValue,
                Discount = orderCustomer.Discount,
                VoucherUsage = orderCustomer.VoucherUsage,
                OrderItems = new List<OrderItemDTO>(),
                Address = new AddressDTO()
            };

            foreach (var item in orderCustomer.OrderItems)
            {
                orderCustomerDTO.OrderItems.Add(new OrderItemDTO
                {
                    Name = item.Name,
                    Image = item.Image,
                    Quantity = item.Quantity,
                    ProductId = item.ProductId,
                    Value = item.Value,
                    OrderId = item.OrderId
                });
            }

            orderCustomerDTO.Address = new AddressDTO
            {
                Street = orderCustomer.Address.Street,
                Number = orderCustomer.Address.Number,
                Complement = orderCustomer.Address.Complement,
                Neighborhood = orderCustomer.Address.Neighborhood,
                ZipCode = orderCustomer.Address.ZipCode,
                City = orderCustomer.Address.City,
                State = orderCustomer.Address.State,
            };

            return orderCustomerDTO;
        }
    }
}
