using GM.ShopFlow.Order.Domain.Enums;

namespace GM.ShopFlow.Order.Domain.States.Order;

public class CreatedOrderState : OrderState
{
    public override OrderStatus Status => OrderStatus.Created;

    public override void Cancel(Entities.Order order)
    {
        order.TransitionTo(new CanceledOrderState());
    }

    public override void Send(Entities.Order order)
    {
        order.TransitionTo(new ShippedOrderState());
    }
}
