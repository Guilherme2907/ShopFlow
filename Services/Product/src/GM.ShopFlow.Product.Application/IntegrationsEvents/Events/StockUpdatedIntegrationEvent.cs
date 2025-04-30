using GM.ShopFlow.Shared.EventBus.Events;

namespace GM.ShopFlow.Product.Application.IntegrationsEvents.Events;

public class StockUpdatedIntegrationEvent(Guid productId, int quantity) : IntegrationEvent
{
    public Guid ProductId { get; set; } = productId;
    public int Quantity { get; set; } = quantity;
}
