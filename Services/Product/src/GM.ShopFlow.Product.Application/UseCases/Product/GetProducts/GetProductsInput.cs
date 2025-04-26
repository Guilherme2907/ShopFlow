using MediatR;

namespace GM.ShopFlow.Product.Application.UseCases.Product.GetProducts;

public record GetProductsInput : IRequest<List<GetProductsOutput>>;
