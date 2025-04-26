using GM.ShopFlow.Product.Application.Interfaces;
using GM.ShopFlow.Product.Domain.SeedWork.Repositories;
using Entities = GM.ShopFlow.Product.Domain.Entities;

namespace GM.ShopFlow.Product.Application.UseCases.Product.CreateProduct;

public class CreateProduct(
    IProductRepository productRepository,
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork
) : ICreateProduct
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(CreateProductInput request, CancellationToken cancellationToken)
    {
        var product = new Entities.Product(request.Name, request.Price);

        if (request.CategoryIds is not null && request.CategoryIds.Any())
        {
            var categories = await _categoryRepository.GetByIdsAsync(request.CategoryIds, cancellationToken);

            categories.ForEach(product.AddCategory);
        }

        await _productRepository.CreateAsync(product, cancellationToken);

        //Throw an event ProductCreated

        await _unitOfWork.CommitAsync();
    }
}
