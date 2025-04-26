using GM.ShopFlow.Product.Domain.Events;
using GM.ShopFlow.Product.Domain.SeedWork;

namespace GM.ShopFlow.Product.Domain.Entities;

public class Product : Entity
{
    public string Name { get; private set; }

    public decimal Price { get; private set; }

    public int Quantity { get; private set; } = 0;

    private readonly IList<Category> _categories = [];

    public IReadOnlyList<Category> Categories => _categories.AsReadOnly();

    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;

        Validate();

        RaiseEvent(new ProductCreatedDomainEvent());
    }

    private void Validate()
    {
        if (_categories is null)
            throw new InvalidDataException("Product must have at least one category");
    }

    private Product() { }

    public void Update(string? name = null, decimal? price = null)
    {
        Name = name ?? Name;
        Price = price ?? Price;
    }

    public void AddCategory(Category category)
    {
        _categories.Add(category);
    }

    public void AddQuantity(int quantity)
    {
        Quantity += quantity;
    }  
    
    public void WithdrawQuantity(int quantity)
    {
        Quantity -= quantity;
    }
}
