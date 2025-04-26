using MediatR;

namespace GM.ShopFlow.Product.Application.UseCases.Product.GetProductById;

public record GetProductByIdInput(Guid Id) : IRequest<GetProductByIdOutput>;

