namespace GM.ShopFlow.Order.Application.Models;
public class ProductStock(string productId, int quantity)
{
    public string ProductId { get; set; } = productId;
    public int Quantity { get; set; } = quantity;
}