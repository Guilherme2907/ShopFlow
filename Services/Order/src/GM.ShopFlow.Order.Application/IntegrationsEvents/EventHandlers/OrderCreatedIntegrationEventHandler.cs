using EventBus.Abstractions;
using GM.ShopFlow.Order.Application.IntegrationsEvents.Events;

namespace GM.ShopFlow.Order.Application.IntegrationsEvents.EventHandlers;

public class OrderCreatedIntegrationEventHandler
    : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
{
    public Task HandleAsync(OrderCreatedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
