using GM.ShopFlow.Order.Application.Interfaces;
using GM.ShopFlow.Order.Infra.Data.Context;

namespace GM.ShopFlow.Order.Infra.Data;

public class UnitOfWork(OrderDbContext context) : IUnitOfWork
{
    private readonly OrderDbContext _context = context;
    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}
