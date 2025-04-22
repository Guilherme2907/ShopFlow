using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using GM.ShopFlow.Product.Infra.Data.Context;
using Microsoft.Extensions.Configuration;

namespace GM.ShopFlow.Product.Infra.Data.Factory;
public class ProductDbContextFactory(IConfiguration configuration) : IDesignTimeDbContextFactory<ProductDbContext>
{
    private readonly IConfiguration _configuration = configuration;

    public ProductDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ProductDbContext>();
        optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:DefaultConnection"]);

        return new ProductDbContext(optionsBuilder.Options);
    }
}
