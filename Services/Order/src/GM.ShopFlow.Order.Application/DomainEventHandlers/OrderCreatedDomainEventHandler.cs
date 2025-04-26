using GM.ShopFlow.Order.Domain.Events;
using MediatR;

namespace GM.ShopFlow.Order.Application.DomainEventHandlers;

public class OrderCreatedDomainEventHandler : INotificationHandler<OrderCreatedDomainEvent>
{
    public Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
