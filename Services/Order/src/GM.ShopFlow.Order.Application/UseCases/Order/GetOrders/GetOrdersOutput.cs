using Entities = GM.ShopFlow.Order.Domain.Entities;

namespace GM.ShopFlow.Order.Application.UseCases.Order.GetOrders;

public record GetOrdersOutput(
    string CustomerName,
    string Status,
    float PercentageDiscount,
    IEnumerable<GetOrderItemOutput> Items
)
{
    public static GetOrdersOutput FromOrder(Entities.Order order)
    {
        return new(
            order.Customer.Name,
            order.Status.ToString(),
            order.PercentageDiscount,
            order.Items.Select(GetOrderItemOutput.FromOrderItem)
        );
    }
}