using EventBus.Abstractions;
using EventBus.Events;
using Microsoft.Extensions.Hosting;

namespace EventBusRabbitMQ;

public class RabbitMQEventBus : BackgroundService, IEventBus
{
    public Task PublishAsync(IntegrationEvent @event, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}