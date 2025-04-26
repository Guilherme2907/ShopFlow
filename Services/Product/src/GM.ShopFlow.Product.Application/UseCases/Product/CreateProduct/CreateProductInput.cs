using MediatR;

namespace GM.ShopFlow.Product.Application.UseCases.Product.CreateProduct;

public record CreateProductInput(
    string Name,
    decimal Price,
    List<Guid>? CategoryIds = null
) : IRequest;

