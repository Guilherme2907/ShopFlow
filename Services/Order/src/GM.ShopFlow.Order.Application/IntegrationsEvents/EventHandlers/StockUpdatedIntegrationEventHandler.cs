using EventBus.Abstractions;
using GM.ShopFlow.Order.Application.IntegrationsEvents.Events;

namespace GM.ShopFlow.Order.Application.IntegrationsEvents.EventHandlers;

public class StockUpdatedIntegrationEventHandler
    : IIntegrationEventHandler<StockUpdatedIntegrationEvent>
{
    public Task HandleAsync(StockUpdatedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
