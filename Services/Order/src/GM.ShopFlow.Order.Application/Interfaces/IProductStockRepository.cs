using GM.ShopFlow.Order.Application.Models;

namespace GM.ShopFlow.Order.Application.Interfaces;
public interface IProductStockRepository
{
    Task SaveAsync(IEnumerable<ProductStock> productStocks, CancellationToken cancellationToken = default);

    Task SetStockAsync(string productId, int newQuantity, CancellationToken cancellationToken = default);

    Task<int?> GetStockAsync(string productId, CancellationToken cancellationToken = default);

    Task<bool> HasProductStocksAsync(CancellationToken cancellationToken = default);
}
