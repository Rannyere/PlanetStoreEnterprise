using Dapper;
using PSE.Order.API.Application.DTOs;
using PSE.Order.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSE.Order.API.Application.Queries;

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
        const string sql = @"SELECT O.Id AS OrderId, O.Code, O.VoucherUsage, O.Discount, O.TotalValue, O.OrderStatus,
                                O.Street, O.Number, O.Neighborhood, O.ZipCode, O.Complement, O.City, O.State, OIT.Id AS OrderItemId,
                                OIT.Name, OIT.Quantity, OIT.Image, OIT.Value 
                            FROM Orders O 
                            INNER JOIN OrderItems OIT ON O.Id = OIT.OrderId 
                            WHERE O.CustomerId = @customerId 
                            AND O.DateRegister >= DATEADD(MINUTE, -1, GETDATE()) 
                            AND O.OrderStatus = 1 
                            ORDER BY O.DateRegister DESC;
";

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
                                O.Id AS OrderId, 
                                O.Id, 
                                O.CustomerId, 
                                OI.Id AS OrderItemId, 
                                OI.Id, 
                                OI.ProductId, 
                                OI.Quantity 
                            FROM Orders O 
                            INNER JOIN OrderItems OI ON O.Id = OI.OrderId
                            WHERE O.OrderStatus = 1                                
                            ORDER BY O.DateRegister";

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
        var testCode = result[0].Code;
        var testOrderId = result[0].OrderId;
        var order = new OrderCustomerDTO
        {
            Id = result[0].OrderId,
            Code = Convert.ToInt32(testCode),
            OrderStatus = Convert.ToInt32(result[0].OrderStatus),
            TotalValue = Convert.ToDecimal(result[0].TotalValue),
            Discount = Convert.ToDecimal(result[0].Discount),
            VoucherUsage = Convert.ToBoolean(result[0].VoucherUsage),

            OrderItems = new List<OrderItemDTO>(),
            Address = new AddressDTO
            {
                Street = result[0].Street,
                Neighborhood = result[0].Neighborhood,
                ZipCode = result[0].ZipCode,
                City = result[0].City,
                Complement = result[0].Complement,
                State = result[0].State,
                Number = result[0].Number
            }
        };

        foreach (var item in result)
        {
            var orderItem = new OrderItemDTO
            {
                Name = item.Name,
                Value = Convert.ToDecimal(item.Value),
                Quantity = Convert.ToInt32(item.Quantity),
                Image = item.Image
            };

            order.OrderItems.Add(orderItem);
        }

        return order;
    }
}