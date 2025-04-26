using EventBus.Abstractions;
using GM.ShopFlow.Product.Application.IntegrationsEvents.Events;

namespace GM.ShopFlow.Product.Application.IntegrationsEvents.EventHandlers;

public class OrderCreatedIntegrationEventHandler
    : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
{
    public Task HandleAsync(OrderCreatedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
