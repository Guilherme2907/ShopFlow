using GM.ShopFlow.Product.Domain.Events;
using GM.ShopFlow.Product.Domain.SeedWork;

namespace GM.ShopFlow.Product.Domain.Entities;

public class Stock : Entity
{
    public Product Product { get; private set; }

    public Guid ProductId { get; private set; }

    public int Quantity { get; private set; } = 0;

    public Stock(Product product)
    {
        Product = product;
    }

    private Stock() { }

    public void AddProductToStock(int quantity)
    {
        Quantity += quantity;

        RaiseDomainEvent(new StockUpdatedDomainEvent(this));
    }

    public void RemoveProductFromStock(int quantity)
    {
        if (Quantity < quantity)
        {
            throw new InvalidOperationException($"Quantity of product {Product.Name} unavailable");
        }

        Quantity -= quantity;

        RaiseDomainEvent(new StockUpdatedDomainEvent(this));
    }
}
