using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PSE.Order.API.Application.DTOs;
using PSE.Order.Domain.Orders;

namespace PSE.Order.API.Application.Queries
{
    public interface IOrderQueries
    {
        Task<OrderCustomerDTO> GetLastOrder(Guid customerId);
        Task<IEnumerable<OrderCustomerDTO>> GetListOrdersByCustomer(Guid customerId);
    }

    public class OrderQueries : IOrderQueries
    {
        private readonly IOrderRepository _orderRepository;

        public OrderQueries(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderCustomerDTO> GetLastOrder(Guid customerId)
        {
            const string sql = @"SELECT
                                O.ID AS 'ProductId', O.CODE, O.VOUCHERUSAGE, O.DISCOUNT, O.TOTALVALUE, O.ORDERSTATUS,
                                O.STREET, O.NUMBER, O.NEIGHBORHOODTY, O.ZIPCODE, O.COMPLEMENT, O.CITY, O.STATE,
                                OIT.ID AS 'ProductItemId',OIT.PRODUCTNAME, OIT.QUANTITY, OIT.PRODUCTIMAGE, OIT.VALUEUNIT 
                                FROM ORDERS O 
                                INNER JOIN ORDERITEMS OIT ON O.ID = OIT.ORDERID 
                                WHERE O.CUSTOMERID = @customerId 
                                AND O.DATEREGISTER >= DATE_SUB(NOW(),INTERVAL 5 MINUTE)
                                AND O.ORDERSTATUS = 1 
                                ORDER BY O.DATEREGISTER DESC";

            var order = await _orderRepository.GetConnection()
                .QueryAsync<dynamic>(sql, new { customerId });

            return MappingOrder(order);
        }

        public async Task<IEnumerable<OrderCustomerDTO>> GetListOrdersByCustomer(Guid customerId)
        {
            var orders = await _orderRepository.GetListOrdersByCustomer(customerId);

            return orders.Select(OrderCustomerDTO.ToOrderCustomerDTO);
        }

        private OrderCustomerDTO MappingOrder(dynamic result)
        {
            var order = new OrderCustomerDTO
            {
                Code = result[0].CODE,
                OrderStatus = result[0].ORDERSTATUS,
                TotalValue = result[0].TOTALVALUE,
                Discount = result[0].DISCOUNT,
                VoucherUsage = result[0].VOUCHERUSAGE,

                OrderItems = new List<OrderItemDTO>(),
                Address = new AddressDTO
                {
                    Street = result[0].STREET,
                    Neighborhoodty = result[0].NEIGHBORHOODTY,
                    ZipCode = result[0].ZIPCODE,
                    City = result[0].CITY,
                    Complement = result[0].COMPLEMENT,
                    State = result[0].STATE,
                    Number = result[0].NUMBER
                }
            };

            foreach (var item in result)
            {
                var orderItem = new OrderItemDTO
                {
                    ProductName = item.PRODUCTNAME,
                    ValueUnit = item.VALUEUNIT,
                    Quantity = item.QUANTITY,
                    ProductImage = item.PRODUCTIMAGE
                };

                order.OrderItems.Add(orderItem);
            }

            return order;
        }
    }
}
