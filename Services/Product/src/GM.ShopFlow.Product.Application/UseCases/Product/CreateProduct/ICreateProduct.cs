using GM.ShopFlow.Product.Domain.SeedWork.Repositories;
using Entities = GM.ShopFlow.Product.Domain.Entities;

using MediatR;

namespace GM.ShopFlow.Product.Application.UseCases.Product.CreateProduct;

public interface ICreateProduct : IRequestHandler<CreateProductInput>
{
}
