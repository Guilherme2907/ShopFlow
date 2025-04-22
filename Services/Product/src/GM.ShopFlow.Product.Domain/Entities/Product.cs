using GM.ShopFlow.Product.Domain.SeedWork;

namespace GM.ShopFlow.Product.Domain.Entities;

public class Product(string name, decimal price) : Entity
{
    public string Name { get; private set; } = name;

    public decimal Price { get; private set; } = price;

    private readonly IList<Category> _categories = [];

    public IReadOnlyList<Category> Categories => _categories.AsReadOnly();

    public void Update(string? name = null, decimal? price = null)
    {
        Name = name ?? Name;
        Price = price ?? Price;
    }

    public void AddCategory(Category category)
    {
        _categories.Add(category);
    }
}
