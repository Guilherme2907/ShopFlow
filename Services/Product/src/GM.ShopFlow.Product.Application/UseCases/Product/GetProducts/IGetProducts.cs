using MediatR;

namespace GM.ShopFlow.Product.Application.UseCases.Product.GetProducts;

public interface IGetProducts : IRequestHandler<GetProductsInput, List<GetProductsOutput>>
{
}
