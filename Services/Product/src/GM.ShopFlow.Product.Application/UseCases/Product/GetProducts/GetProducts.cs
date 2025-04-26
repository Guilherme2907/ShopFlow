using GM.ShopFlow.Product.Domain.SeedWork.Repositories;


namespace GM.ShopFlow.Product.Application.UseCases.Product.GetProducts;

public class GetProducts(IProductRepository productRepository) : IGetProducts
{
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<List<GetProductsOutput>> Handle(GetProductsInput request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAsync(cancellationToken);

        //Check if products is null

        return products.Select(GetProductsOutput.FromProduct).ToList();
    }
}
