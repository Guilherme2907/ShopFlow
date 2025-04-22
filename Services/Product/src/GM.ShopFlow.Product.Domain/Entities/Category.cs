using GM.ShopFlow.Product.Domain.SeedWork;

namespace GM.ShopFlow.Product.Domain.Entities;

public class Category(string name) : Entity
{
    public string Name { get; private set; } = name;

    public void Update(string name)
    {
        Name = name;
    }
}
