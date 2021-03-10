using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PSE.Core.Data;

namespace PSE.Order.Domain.Orders
{
    public interface IOrderRepository : IRepository<OrderCustomer>
    {
        Task<OrderCustomer> GetById(Guid id);
        Task<IEnumerable<OrderCustomer>> GetListOrdersByCustomer(Guid customerId);
        void Add(OrderCustomer order);
        void Update(OrderCustomer order);

        /* Order Item */
        Task<OrderItem> GetItemById(Guid id);
        Task<OrderItem> GetItemByOrder(Guid orderId, Guid productId);
    }
}
