using Entities =  GM.ShopFlow.Product.Domain.Entities;
using GM.ShopFlow.Product.Domain.SeedWork.Repositories;
using GM.ShopFlow.Product.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using GM.ShopFlow.Product.Domain.Entities;

namespace GM.ShopFlow.Product.Infra.Data.Repositories;
public class ProductRepository(ProductDbContext context) : IProductRepository
{
    private readonly ProductDbContext _context = context;

    private DbSet<Entities.Product> Products => _context.Products;

    public async Task CreateAsync(Entities.Product product, CancellationToken cancellationToken)
    {
        await Products.AddAsync(product, cancellationToken);
    }  
    
    public async Task UpdateAsync(Entities.Product product, CancellationToken cancellationToken)
    {
        await Task.FromResult(Products.Update(product));
    }

    public async Task<List<Entities.Product>> GetAsync(CancellationToken cancellationToken)
    {
        return await Products.ToListAsync(cancellationToken);
    }

    public async Task<Entities.Product> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var product = await Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        return product is null ? throw new InvalidDataException($"There is no product with the id {id}") : product;
    }
}
