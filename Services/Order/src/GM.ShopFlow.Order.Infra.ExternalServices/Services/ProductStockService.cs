using GM.ShopFlow.Order.Application.Interfaces;
using GM.ShopFlow.Order.Application.Models;
using GM.ShopFlow.Order.Infra.ExternalServices.Apis;

namespace GM.ShopFlow.Order.Infra.ExternalServices.Services;

public class ProductStockService(
    IProductApi productApi,
    IProductStockRepository productStockRepository
) : IProductStockService
{
    private readonly IProductApi _productApi = productApi;
    private readonly IProductStockRepository _productStockRepository = productStockRepository;

    public async Task PopulateProductStocksDbAsync(CancellationToken cancellationToken)
    {
        var products = await _productApi.GetProductsAsync(cancellationToken);

        var productStocks =  products.Select(p => 
            new ProductStock(p.Id.ToString(), p.Quantity)
        );

        await _productStockRepository.SaveAsync(productStocks);
    }
}
