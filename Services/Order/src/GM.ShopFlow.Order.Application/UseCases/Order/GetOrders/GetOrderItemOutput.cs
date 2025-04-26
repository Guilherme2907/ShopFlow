using GM.ShopFlow.Order.Domain.Entities;

namespace GM.ShopFlow.Order.Application.UseCases.Order.GetOrders;

public record GetOrderItemOutput(
    int Quantity,
    string ProductName,
    decimal ProductPrice,
    decimal Total
)
{
    public static GetOrderItemOutput FromOrderItem(OrderItem item)
    {
        return new GetOrderItemOutput(
            item.Quantity,
            item.ProductName,
            item.ProductPrice,
            item.Total
        );
    }
}
