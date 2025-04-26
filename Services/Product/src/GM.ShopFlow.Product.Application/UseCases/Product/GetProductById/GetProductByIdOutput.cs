using Entities = GM.ShopFlow.Product.Domain.Entities;

namespace GM.ShopFlow.Product.Application.UseCases.Product.GetProductById;

public record GetProductByIdOutput(Guid Id, string Name, decimal Price)
{
    public static GetProductByIdOutput FromProduct(Entities.Product product)
    {
        return new(
            product.Id,
            product.Name,
            product.Price
        );
    }
}
