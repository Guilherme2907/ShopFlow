using GM.ShopFlow.Order.Infra.ExternalServices.Models.Product;
using RestEase;

namespace GM.ShopFlow.Order.Infra.ExternalServices.Apis;

public interface IProductApi
{
    [Get]
    Task<IEnumerable<ProductsResponse>> GetProductsAsync(CancellationToken cancellationToken = default);
}
