using GM.ShopFlow.Product.Domain.Entities;

namespace GM.ShopFlow.Product.Domain.SeedWork.Repositories;

public interface ICategoryRepository
{
    Task CreateAsync(Category category, CancellationToken cancellationToken);

    Task<List<Category>> GetAsync(CancellationToken cancellationToken);

    Task<List<Category>> GetByIdsAsync(IEnumerable<Guid> Ids, CancellationToken cancellationToken);
}
