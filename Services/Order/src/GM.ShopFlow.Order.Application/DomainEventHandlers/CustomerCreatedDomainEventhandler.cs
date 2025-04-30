using GM.ShopFlow.Order.Application.IntegrationsEvents.Events;
using GM.ShopFlow.Order.Domain.DomainEvents;
using GM.ShopFlow.Shared.EventBus.Abstractions;
using MediatR;

namespace GM.ShopFlow.Order.Application.DomainEventHandlers;

public class CustomerCreatedDomainEventhandler(IEventBus eventBus) : INotificationHandler<CustomerCreatedDomainEvent>
{
    private readonly IEventBus _eventBus = eventBus;

    public async Task Handle(CustomerCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        await _eventBus.PublishAsync(new CustomerCreatedIntegrationEvent(domainEvent.Customer.UserId), cancellationToken);
    }
}
