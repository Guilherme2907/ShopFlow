using EventBus.Events;

namespace EventBus.Abstractions;

public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
    where TIntegrationEvent : IntegrationEvent
{
    Task HandleAsync(TIntegrationEvent @event, CancellationToken cancellationToken);

    Task IIntegrationEventHandler.HandleAsync(IntegrationEvent @event, CancellationToken cancellationToken)
        => HandleAsync(@event, cancellationToken);
}

public interface IIntegrationEventHandler
{
    Task HandleAsync(IntegrationEvent @event, CancellationToken cancellationToken);
}
