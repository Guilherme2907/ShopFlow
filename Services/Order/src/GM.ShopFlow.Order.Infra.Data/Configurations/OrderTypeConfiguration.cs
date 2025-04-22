using GM.ShopFlow.Order.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entity = GM.ShopFlow.Order.Domain.Entities;

namespace GM.ShopFlow.Order.Infra.Data.Configurations;

public class OrderTypeConfiguration : IEntityTypeConfiguration<Entity.Order>
{
    public void Configure(EntityTypeBuilder<Entity.Order> entity)
    {
        entity.ToTable("Orders");

        entity.Property(e => e.Status)
            .HasConversion(
                fromObj => fromObj.ToString(),
                fromDb => (OrderStatus)Enum.Parse(typeof(OrderStatus), fromDb)
            );

        entity.Ignore(e => e.Events);

        entity.HasMany(e => e.Items)
            .WithOne(e => e.Order)
            .HasForeignKey(e => e.OrderId);

        entity.HasOne(e => e.Customer)
            .WithMany(e => e.Orders)
            .HasForeignKey(e => e.CustomerId);
    }
}
