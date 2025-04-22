using GM.ShopFlow.Order.Domain.Enums;

namespace GM.ShopFlow.Order.Domain.States.Order;

public class CanceledOrderState : OrderState
{
    public override OrderStatus Status => OrderStatus.Canceled;
}
