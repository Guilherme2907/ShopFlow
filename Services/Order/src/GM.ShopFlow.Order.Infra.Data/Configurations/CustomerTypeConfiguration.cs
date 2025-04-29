using GM.ShopFlow.Order.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GM.ShopFlow.Order.Infra.Data.Configurations;

public class CustomerTypeConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> entity)
    {
        entity.ToTable("Customers");

        entity.Property(e => e.Name)
            .HasMaxLength(50)
            .IsRequired();

        entity.OwnsOne(e => e.Email, email =>
        {
            email.Property(e => e.Value).HasColumnName("Email");
            email.HasIndex(e => e.Value).IsUnique();
        });

        entity.OwnsOne(e => e.CpfOrCnpj, c =>
        {
            c.Property(e => e.Value).HasColumnName("CpfOrCnpj");
            c.HasIndex(e => e.Value).IsUnique();
        });

        entity.Ignore(e => e.DomainEvents);
    }
}
