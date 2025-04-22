using GM.ShopFlow.Order.Domain.Enums;

namespace GM.ShopFlow.Order.Domain.States.Order;

public class ShippedOrderState : OrderState
{
    public override OrderStatus Status => OrderStatus.Shipping;

    public override void Cancel(Entities.Order order)
    {
        order.TransitionTo(new CanceledOrderState());
    }

    public override void Finish(Entities.Order order)
    {
        order.TransitionTo(new FinishedOrderState());
    }
}
