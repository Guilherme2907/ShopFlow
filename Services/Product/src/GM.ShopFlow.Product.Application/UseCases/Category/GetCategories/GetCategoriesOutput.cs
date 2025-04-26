using Entities = GM.ShopFlow.Product.Domain.Entities;

namespace GM.ShopFlow.Product.Application.UseCases.Category.GetCategories;

public record GetCategoriesOutput(Guid Id, string Name)
{
    public static GetCategoriesOutput FromCategory(Entities.Category category)
    {
        return new(
            category.Id,
            category.Name
        );
    }
}
