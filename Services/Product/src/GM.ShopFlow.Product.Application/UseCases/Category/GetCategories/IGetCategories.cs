using MediatR;

namespace GM.ShopFlow.Product.Application.UseCases.Category.GetCategories;

public interface IGetCategories : IRequestHandler<GetCategoriesInput, List<GetCategoriesOutput>>
{
}
