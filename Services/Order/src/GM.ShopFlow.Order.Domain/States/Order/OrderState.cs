using GM.ShopFlow.Order.Domain.Enums;
using Entity = GM.ShopFlow.Order.Domain.Entities;

namespace GM.ShopFlow.Order.Domain.States.Order;

public abstract class OrderState
{
    public abstract OrderStatus Status { get; }

    public virtual void Create(Entity.Order order)
    {
        throw new InvalidOperationException($"Cannot create an order with status {order.Status}");
    }  
    
    public virtual void Send(Entity.Order order)
    {
        throw new InvalidOperationException($"Cannot send an order with status {order.Status}");
    } 
    
    public virtual void Cancel(Entity.Order order)
    {
        throw new InvalidOperationException($"Cannot cancel the order with status {order.Status}");
    }  
    
    public virtual void Finish(Entity.Order order)
    {
        throw new InvalidOperationException($"Cannot finish the order with status {order.Status}");
    }

    public static OrderState FromStatus(OrderStatus status)
    {
        return status switch
        {
            OrderStatus.Created => new CreatedOrderState(),
            OrderStatus.Canceled => new CanceledOrderState(),
            OrderStatus.Shipping => new ShippedOrderState(),
            OrderStatus.Finished => new FinishedOrderState(),
            _ => throw new InvalidOperationException("Invalid status")
        };
    }
}
