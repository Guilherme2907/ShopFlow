using GM.ShopFlow.Shared.EventBus.Events;

namespace GM.ShopFlow.Shared.EventBus.Abstractions;
public interface IEventBus
{
    Task PublishAsync(IntegrationEvent @event, CancellationToken cancellationToken);
}
