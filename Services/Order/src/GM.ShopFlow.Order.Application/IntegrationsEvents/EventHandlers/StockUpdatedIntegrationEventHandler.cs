using EventBus.Abstractions;
using GM.ShopFlow.Order.Application.IntegrationsEvents.Events;
using GM.ShopFlow.Order.Application.Interfaces;

namespace GM.ShopFlow.Order.Application.IntegrationsEvents.EventHandlers;

public class StockUpdatedIntegrationEventHandler(IProductStockRepository productStockRepository)
    : IIntegrationEventHandler<StockUpdatedIntegrationEvent>
{
    private readonly IProductStockRepository _productStockRepository = productStockRepository;

    public async Task HandleAsync(StockUpdatedIntegrationEvent @event)
    {
        await _productStockRepository.SetStockAsync(@event.ProductId.ToString(), @event.Quantity);
    }
}
