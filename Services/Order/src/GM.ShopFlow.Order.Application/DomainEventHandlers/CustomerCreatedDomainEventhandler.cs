using GM.ShopFlow.Order.Domain.Events;
using MediatR;

namespace GM.ShopFlow.Order.Application.DomainEventHandlers;

public class CustomerCreatedDomainEventhandler : INotificationHandler<CustomerCreatedDomainEvent>
{
    public Task Handle(CustomerCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
