using Microsoft.EntityFrameworkCore;
using PSE.Core.Data;
using PSE.Order.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace PSE.Order.Infra.Data.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _context;

    public OrderRepository(OrderDbContext context)
    {
        _context = context;
    }

    public IUnityOfWork UnitOfWork => _context;
    public DbConnection GetConnection() => _context.Database.GetDbConnection();

    public async Task<OrderCustomer> GetById(Guid id)
    {
        return await _context.Orders.FindAsync(id);
    }

    public async Task<OrderItem> GetItemById(Guid id)
    {
        return await _context.OrderItems.FindAsync(id);
    }

    public async Task<OrderItem> GetItemByOrder(Guid orderId, Guid productId)
    {
        return await _context.OrderItems
            .FirstOrDefaultAsync(o => o.ProductId == productId && o.OrderId == orderId);
    }

    public async Task<IEnumerable<OrderCustomer>> GetListOrdersByCustomer(Guid customerId)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .AsNoTracking()
            .Where(o => o.CustomerId == customerId)
            .ToListAsync();
    }

    public void Add(OrderCustomer order)
    {
        _context.Orders.Add(order);
    }

    public void Update(OrderCustomer order)
    {
        _context.Entry(order).State = EntityState.Modified;
        _context.Orders.Update(order);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}