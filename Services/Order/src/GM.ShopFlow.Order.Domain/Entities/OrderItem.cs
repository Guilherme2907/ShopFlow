using GM.ShopFlow.Order.Domain.SeedWork;

namespace GM.ShopFlow.Order.Domain.Entities;

public class OrderItem : Entity
{
    public int Quantity { get; private set; }

    public Order Order { get; private set; }

    public Guid OrderId { get; private set; }

    public Guid ProductId { get; private set; }

    public string ProductName { get; private set; }

    public decimal Price { get; private set; }

    public decimal Total { get; private set; }

    public OrderItem(int quantity, Order order, Guid productId, string productName, decimal price, decimal total)
    {
        Quantity = quantity;
        Order = order;
        ProductId = productId;
        ProductName = productName;
        Price = price;
        Total = total;
    }

    private OrderItem() { }
}
