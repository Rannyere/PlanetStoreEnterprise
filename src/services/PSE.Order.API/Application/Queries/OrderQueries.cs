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
        Task<OrderCustomerDTO> GetOrdersAuthorized();
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
                                O.STREET, O.NUMBER, O.NEIGHBORHOOD, O.ZIPCODE, O.COMPLEMENT, O.CITY, O.STATE,
                                OIT.ID AS 'ProductItemId',OIT.NAME, OIT.QUANTITY, OIT.IMAGE, OIT.VALUE 
                                FROM ORDERS O 
                                INNER JOIN ORDERITEMS OIT ON O.ID = OIT.ORDERID 
                                WHERE O.CUSTOMERID = @customerId 
                                AND O.DATEREGISTER >= DATE_SUB(NOW(),INTERVAL 1 MINUTE)
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

        public async Task<OrderCustomerDTO> GetOrdersAuthorized()
        {
            const string sql = @"SELECT 
                                O.ID as 'OrderId', O.ID, O.CUSTOMERID, 
                                OI.ID as 'OrderItemId', OI.ID, OI.PRODUCTID, OI.QUANTITY 
                                FROM ORDERS O 
                                INNER JOIN ORDERITEMS OI ON O.ID = OI.ORDERID 
                                WHERE O.ORDERSTATUS = 1                                
                                ORDER BY O.DATEREGISTER";

            // Use of the lookup to maintain the status for each record cycle returned
            var lookup = new Dictionary<Guid, OrderCustomerDTO>();

            await _orderRepository.GetConnection().QueryAsync<OrderCustomerDTO, OrderItemDTO, OrderCustomerDTO>(sql,
                (o, oi) =>
                {
                    if (!lookup.TryGetValue(o.Id, out var orderCustomerDTO))
                        lookup.Add(o.Id, orderCustomerDTO = o);

                    orderCustomerDTO.OrderItems ??= new List<OrderItemDTO>();
                    orderCustomerDTO.OrderItems.Add(oi);

                    return orderCustomerDTO;

                }, splitOn: "OrderId,OrderItemId");

            // Getting data from the lookup
            return lookup.Values.OrderBy(p => p.DateRegister).FirstOrDefault();
        }

        private OrderCustomerDTO MappingOrder(dynamic result)
        {
            var order = new OrderCustomerDTO
            {
                Code = Convert.ToInt32(result[0].CODE),
                OrderStatus = Convert.ToInt32(result[0].ORDERSTATUS),
                TotalValue = Convert.ToDecimal(result[0].TOTALVALUE),
                Discount = Convert.ToDecimal(result[0].DISCOUNT),
                VoucherUsage = Convert.ToBoolean(result[0].VOUCHERUSAGE),

                OrderItems = new List<OrderItemDTO>(),
                Address = new AddressDTO
                {
                    Street = result[0].STREET,
                    Neighborhood = result[0].NEIGHBORHOOD,
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
                    Name = item.NAME,
                    Value = item.VALUE,
                    Quantity = item.QUANTITY,
                    Image = item.IMAGE
                };

                order.OrderItems.Add(orderItem);
            }

            return order;
        }
    }
}
