using GM.ShopFlow.Order.Domain.SeedWork;

namespace GM.ShopFlow.Order.Domain.Entities;

public class OrderItem : Entity
{
    public int Quantity { get; private set; }

    public Order Order { get; private set; }

    public Guid OrderId { get; private set; }

    public Guid ProductId { get; private set; }

    public string ProductName { get; private set; }

    public decimal ProductPrice { get; private set; }

    public decimal Total { get; private set; }

    public OrderItem(int quantity, Guid productId, string productName, decimal productPrice)
    {
        Quantity = quantity;
        ProductId = productId;
        ProductName = productName;
        ProductPrice = productPrice;
        Total = CalculateTotal();
    }

    private decimal CalculateTotal()
    {
        return Quantity * ProductPrice;
    }

    private OrderItem() { }
}
