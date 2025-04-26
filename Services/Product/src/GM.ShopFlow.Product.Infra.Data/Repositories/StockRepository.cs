using GM.ShopFlow.Product.Domain.Entities;
using GM.ShopFlow.Product.Domain.SeedWork.Repositories;
using GM.ShopFlow.Product.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GM.ShopFlow.Product.Infra.Data.Repositories;
public class StockRepository(ProductDbContext context) : IStockRepository
{
    private readonly ProductDbContext _context = context;

    private DbSet<Stock> Stocks => _context.Stocks;

    public async Task CreateAsync(Stock stock, CancellationToken cancellationToken)
    {
        await Stocks.AddAsync(stock, cancellationToken);
    }

    public async Task<Stock> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken)
    {
        var stock = await Stocks.FirstOrDefaultAsync(s => s.ProductId == productId, cancellationToken);

        return stock is null ? throw new InvalidDataException($"There is no stock with product Id {productId}") : stock;
    }

    public async Task UpdateAsync(Stock stock, CancellationToken cancellationToken)
    {
        await Task.FromResult(Stocks.Update(stock));
    }
}
