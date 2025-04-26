using GM.ShopFlow.Product.Application.Interfaces;
using GM.ShopFlow.Product.Infra.Data.Context;

namespace GM.ShopFlow.Product.Infra.Data;

public class UnitOfWork(ProductDbContext context) : IUnitOfWork
{
    private readonly ProductDbContext _context = context;
    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }
}
