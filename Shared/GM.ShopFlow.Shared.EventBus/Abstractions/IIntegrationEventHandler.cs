using GM.ShopFlow.Shared.EventBus.Events;

namespace GM.ShopFlow.Shared.EventBus.Abstractions;

public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
    where TIntegrationEvent : IntegrationEvent
{
    Task HandleAsync(TIntegrationEvent @event);

    Task IIntegrationEventHandler.HandleAsync(IntegrationEvent @event)
        => HandleAsync((TIntegrationEvent)@event);
}

public interface IIntegrationEventHandler
{
    Task HandleAsync(IntegrationEvent @event);
}
