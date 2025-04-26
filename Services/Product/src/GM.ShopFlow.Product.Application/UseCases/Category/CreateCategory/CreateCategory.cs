using Entities = GM.ShopFlow.Product.Domain.Entities;
using GM.ShopFlow.Product.Domain.SeedWork.Repositories;
using GM.ShopFlow.Product.Application.Interfaces;

namespace GM.ShopFlow.Product.Application.UseCases.Category.CreateCategory;

public class CreateCategory(
    ICategoryRepository categoryRepository,
    IUnitOfWork unitOfWork
) : ICreateCategory
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(CreateCategoryInput request, CancellationToken cancellationToken)
    {
        var category = new Entities.Category(request.Name);

        await _categoryRepository.CreateAsync(category, cancellationToken);

        await _unitOfWork.CommitAsync();
    }
}
