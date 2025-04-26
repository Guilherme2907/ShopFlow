using Entities = GM.ShopFlow.Product.Domain.Entities;

namespace GM.ShopFlow.Product.Application.UseCases.Product.GetProducts;

public record GetProductsOutput(Guid Id, string Name, decimal Price, int Quantity)
{
    public static GetProductsOutput FromProduct(Entities.Product product)
    {
        return new(
            product.Id,
            product.Name,
            product.Price,
            product.Quantity
        );
    }
}
