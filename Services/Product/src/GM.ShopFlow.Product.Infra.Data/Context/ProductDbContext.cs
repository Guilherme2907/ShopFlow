using GM.ShopFlow.Product.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Entity = GM.ShopFlow.Product.Domain.Entities;

namespace GM.ShopFlow.Product.Infra.Data.Context;
public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
{
    public DbSet<Entity.Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Stock> Stocks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductDbContext).Assembly);
    }
}
