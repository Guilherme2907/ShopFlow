using GM.ShopFlow.Product.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GM.ShopFlow.Product.Infra.Data.Configurations;
internal class StockTypeConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> entity)
    {
        entity.ToTable("Stocks");

        entity.Property(entity => entity.ProductId)
            .HasColumnName("ProductId")
            .IsRequired();

        entity.HasIndex(entity => entity.ProductId)
            .IsUnique();

        entity.Property(e => e.Quantity)
            .HasColumnName("Quantity")
            .IsRequired();

        entity.HasOne(e => e.Product);

        entity.Ignore(e => e.Events);
    }
}
