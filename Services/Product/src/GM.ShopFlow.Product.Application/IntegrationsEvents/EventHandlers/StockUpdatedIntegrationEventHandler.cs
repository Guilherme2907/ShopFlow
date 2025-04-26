using EventBus.Abstractions;
using GM.ShopFlow.Product.Application.IntegrationsEvents.Events;

namespace GM.ShopFlow.Product.Application.IntegrationsEvents.EventHandlers;

public class StockUpdatedIntegrationEventHandler
    : IIntegrationEventHandler<StockUpdatedIntegrationEvent>
{
    public Task HandleAsync(StockUpdatedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
