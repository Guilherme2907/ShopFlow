using MediatR;

namespace GM.ShopFlow.Product.Application.UseCases.Category.CreateCategory;

public record CreateCategoryInput(string Name) : IRequest;

