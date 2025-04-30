using GM.ShopFlow.Order.Application.Models;

namespace GM.ShopFlow.Order.Application.Interfaces;

public interface IProductStockService
{
    Task PopulateProductStocksDbAsync(CancellationToken cancellationToken = default);
}
