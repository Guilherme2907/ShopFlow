using GM.ShopFlow.Product.Domain.SeedWork.Repositories;

namespace GM.ShopFlow.Product.Application.UseCases.Product.GetProductById;

public class GetProductById(IProductRepository productRepository) : IGetProductById
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<GetProductByIdOutput> Handle(GetProductByIdInput request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

        // Check if product is null

        return GetProductByIdOutput.FromProduct(product);
    }
}
