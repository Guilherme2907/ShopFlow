using Entities = GM.ShopFlow.Product.Domain.Entities;

namespace GM.ShopFlow.Product.Domain.SeedWork.Repositories;

public interface IProductRepository
{
    Task CreateAsync(Entities.Product product, CancellationToken cancellationToken = default);

    Task UpdateAsync(Entities.Product product, CancellationToken cancellationToken = default);

    Task<List<Entities.Product>> GetAsync(CancellationToken cancellationToken = default);

    Task<Entities.Product> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
