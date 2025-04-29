using GM.ShopFlow.Product.Domain.Entities;

namespace GM.ShopFlow.Product.Domain.SeedWork.Repositories;

public interface IStockRepository
{
    Task CreateAsync(Stock stock, CancellationToken cancellationToken = default);

    Task UpdateAsync(Stock stock, CancellationToken cancellationToken = default);

    Task UpdateAsync(IEnumerable<Stock> stocks, CancellationToken cancellationToken = default);

    Task<Stock> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);

    Task<IEnumerable<Stock>> GetByProductIdsAsync(IEnumerable<Guid> productIds, CancellationToken cancellationToken = default);
}
