using Entities = GM.ShopFlow.Product.Domain.Entities;

namespace GM.ShopFlow.Product.Domain.SeedWork.Repositories;

public interface IProductRepository
{
    Task CreateAsync(Entities.Product product, CancellationToken cancellationToken);

    Task<List<Entities.Product>> GetAsync(CancellationToken cancellationToken);

    Task<Entities.Product> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
