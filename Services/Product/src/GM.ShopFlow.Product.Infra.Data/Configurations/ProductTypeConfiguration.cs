using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entity = GM.ShopFlow.Product.Domain.Entities;

namespace GM.ShopFlow.Product.Infra.Data.Configurations;
internal class ProductTypeConfiguration : IEntityTypeConfiguration<Entity.Product>
{
    public void Configure(EntityTypeBuilder<Entity.Product> entity)
    {
        entity.ToTable("Products");
        entity.Property(entity => entity.Name)
            .HasColumnName("Name")
            .IsRequired();

        entity.Property(e => e.Price)
            .HasColumnName("Price")
            .HasColumnType("decimal")
            .IsRequired();

        entity.HasMany(c => c.Categories)
            .WithMany();

        entity.Ignore(e => e.DomainEvents);
    }
}
