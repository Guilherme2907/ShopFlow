using GM.ShopFlow.Order.Domain.Enums;

namespace GM.ShopFlow.Order.Domain.States.Order;

public class FinishedOrderState : OrderState
{
    public override OrderStatus Status => OrderStatus.Finished;
}
