using GM.ShopFlow.Product.Domain.Entities;
using GM.ShopFlow.Product.Domain.SeedWork.Repositories;
using GM.ShopFlow.Product.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GM.ShopFlow.Product.Infra.Data.Repositories;

public class CategoryRepository(ProductDbContext context) : ICategoryRepository
{
    private readonly ProductDbContext _context = context;

    private DbSet<Category> Categories => _context.Categories;

    public async Task CreateAsync(Category category, CancellationToken cancellationToken)
    {
        await Categories.AddAsync(category, cancellationToken);
    }

    public async Task<List<Category>> GetAsync(CancellationToken cancellationToken)
    {
        return await Categories.ToListAsync(cancellationToken);
    }

    public async Task<List<Category>> GetByIdsAsync(IEnumerable<Guid> Ids, CancellationToken cancellationToken)
    {
        var categories = await Categories
            .Where(c => Ids.Contains(c.Id))
            .ToListAsync(cancellationToken);

        if (categories.Count != Ids.Count())
            throw new InvalidOperationException("One or more categories is invalid");

        return categories;
    }
}
