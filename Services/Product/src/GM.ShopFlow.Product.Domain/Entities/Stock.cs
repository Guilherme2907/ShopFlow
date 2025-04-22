using GM.ShopFlow.Product.Domain.SeedWork;

namespace GM.ShopFlow.Product.Domain.Entities;

public class Stock : Entity
{
    public Product Product { get; private set; }

    public Guid ProductId { get; private set; } 

    public int Quantity { get; private set; }

    public Stock(Guid productId, Product product, int quantity)
    {
        ProductId = productId;
        Product = product;
        Quantity = quantity;
    }

    private Stock() { }

    public void AddProductToStock(int quantity)
    {
        Quantity += quantity;
    }

    public void RemoveProductFromStock(int quantity)
    {
        if (Quantity < quantity)
        {
            throw new InvalidOperationException($"Quantity of product {Product.Name} unavailable");
        }

        Quantity -= quantity;
    }
}
