using GM.ShopFlow.Product.Domain.Events;
using MediatR;

namespace GM.ShopFlow.Product.Application.DomainEventHandlers;

public class StockUpdatedDomainEventHandler : INotificationHandler<StockUpdatedDomainEvent>
{
    public Task Handle(StockUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
