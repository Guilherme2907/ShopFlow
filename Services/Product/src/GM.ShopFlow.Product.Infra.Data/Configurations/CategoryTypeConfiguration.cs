using GM.ShopFlow.Product.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Entity = GM.ShopFlow.Product.Domain.Entities;

namespace GM.ShopFlow.Product.Infra.Data.Configurations;
internal class CategoryTypeConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> entity)
    {
        entity.ToTable("Categories");

        entity.Property(entity => entity.Name)
            .HasColumnName("Name")
            .IsRequired();

        entity.Ignore(e => e.Events);
    }
}
