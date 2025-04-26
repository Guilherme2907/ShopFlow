using EventBus.Abstractions;
using GM.ShopFlow.Order.Application.IntegrationsEvents.Events;

namespace GM.ShopFlow.Order.Application.IntegrationsEvents.EventHandlers;

public class CustomerCreatedIntegrationEventHandler
    : IIntegrationEventHandler<CustomerCreatedIntegrationEvent>
{
    public Task HandleAsync(CustomerCreatedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
