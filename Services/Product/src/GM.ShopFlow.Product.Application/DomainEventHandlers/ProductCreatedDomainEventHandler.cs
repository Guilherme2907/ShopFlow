using GM.ShopFlow.Product.Domain.Events;
using MediatR;

namespace GM.ShopFlow.Product.Application.DomainEventHandlers;

public class ProductCreatedDomainEventHandler : INotificationHandler<ProductCreatedDomainEvent>
{
    public Task Handle(ProductCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
