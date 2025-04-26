using MediatR;

namespace GM.ShopFlow.Product.Application.UseCases.Product.GetProductById;

public interface IGetProductById : IRequestHandler<GetProductByIdInput, GetProductByIdOutput>
{
}
