using Entities = GM.ShopFlow.Order.Domain.Entities;

using GM.ShopFlow.Order.Domain.SeedWork.Repositories;
using GM.ShopFlow.Order.Infra.Data.Context;
using GM.ShopFlow.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GM.ShopFlow.Order.Infra.Data.Repositories;

public class OrderRepository(OrderDbContext context) : IOrderRepository
{
    private readonly OrderDbContext _context = context;

    private DbSet<Entities.Order> Orders => _context.Orders;

    public async Task CreateAsync(Entities.Order order, CancellationToken cancellationToken)
    {
        await Orders.AddAsync(order, cancellationToken);
    }

    public async Task<List<Entities.Order>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken)
    {
        var orders = await Orders.Where(o => o.CustomerId == customerId)
            .Include(o => o.Customer)
            .Include(o => o.Items)
            .ToListAsync(cancellationToken: cancellationToken);

        return orders is null || !orders.Any() ?
            throw new InvalidDataException($"There is not orders with customer id: {customerId}") :
            orders;
    }
}
