using GM.ShopFlow.Product.Domain.SeedWork.Repositories;

namespace GM.ShopFlow.Product.Application.UseCases.Category.GetCategories;

public class GetCategories(ICategoryRepository categoryRepository) : IGetCategories
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task<List<GetCategoriesOutput>> Handle(GetCategoriesInput request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAsync(cancellationToken);

        //Check if categories is null

        return categories.Select(GetCategoriesOutput.FromCategory).ToList();
    }
}
