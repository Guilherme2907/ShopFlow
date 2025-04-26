using MediatR;

namespace GM.ShopFlow.Product.Application.UseCases.Category.GetCategories;

public record GetCategoriesInput : IRequest<List<GetCategoriesOutput>>;
