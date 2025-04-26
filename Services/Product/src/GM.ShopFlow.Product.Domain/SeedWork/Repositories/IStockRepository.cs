using GM.ShopFlow.Product.Domain.Entities;

namespace GM.ShopFlow.Product.Domain.SeedWork.Repositories;

public interface IStockRepository
{
    Task CreateAsync(Stock stock, CancellationToken cancellationToken);

    Task UpdateAsync(Stock stock, CancellationToken cancellationToken);

    Task<Stock> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken);
}
