
using GM.ShopFlow.Shared.EventBus.Events;

namespace GM.ShopFlow.Order.Application.IntegrationsEvents.Events;

public class CustomerCreatedIntegrationEvent(Guid userId) : IntegrationEvent
{
    public Guid UserId { get; private set; } = userId;
}

