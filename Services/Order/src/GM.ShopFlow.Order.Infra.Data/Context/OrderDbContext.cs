using GM.ShopFlow.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Entity = GM.ShopFlow.Order.Domain.Entities;

namespace GM.ShopFlow.Order.Infra.Data.Context;

public class OrderDbContext(DbContextOptions<OrderDbContext> options) : DbContext(options)
{
    public DbSet<Entity.Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderDbContext).Assembly);
    }
}
