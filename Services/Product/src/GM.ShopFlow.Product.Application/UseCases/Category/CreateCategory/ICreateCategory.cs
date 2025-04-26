using GM.ShopFlow.Product.Domain.SeedWork.Repositories;
using Entities = GM.ShopFlow.Product.Domain.Entities;

using MediatR;

namespace GM.ShopFlow.Product.Application.UseCases.Category.CreateCategory;

public interface ICreateCategory : IRequestHandler<CreateCategoryInput>
{
}
