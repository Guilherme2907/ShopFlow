using GM.ShopFlow.Order.Application.IntegrationsEvents.Events;
using GM.ShopFlow.Order.Domain.DomainEvents;
using GM.ShopFlow.Shared.EventBus.Abstractions;
using MediatR;

namespace GM.ShopFlow.Order.Application.DomainEventHandlers;

public class OrderCreatedDomainEventHandler(IEventBus eventBus) : INotificationHandler<OrderCreatedDomainEvent>
{
    private readonly IEventBus _eventBus = eventBus;

    public async Task Handle(OrderCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var integrationEvent = new OrderCreatedIntegrationEvent(
            domainEvent.Order.Id,
            domainEvent.Order.CustomerId,
            domainEvent.Order.Items.Select(i => new OrderStockItem(i.ProductId, i.Quantity))
        );

        await _eventBus.PublishAsync(integrationEvent, cancellationToken);
    }
}
