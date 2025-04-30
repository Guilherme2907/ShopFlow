using GM.ShopFlow.Shared.EventBus.Abstractions;
using GM.ShopFlow.Shared.EventBus.Events;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text.Json;

namespace GM.ShopFlow.Shared.EventBusRabbitMQ;

public class RabbitMQEventBusProducer(
    IConnection connection,
    IOptions<EventBusSubscriptionInfo> subscriptionInfo
) : IEventBus
{
    private readonly IConnection _connection = connection;
    private readonly EventBusSubscriptionInfo _subscriptionInfo = subscriptionInfo.Value;

    private const string ExchangeName = "shopflow_exchange";

    public Task PublishAsync(IntegrationEvent @event, CancellationToken cancellationToken)
    {
        var routingKey = @event.GetType().Name;

        using var channel = _connection.CreateModel() ??
            throw new InvalidOperationException("RabbitMQ connection is not open");

        channel.ExchangeDeclare(
            exchange: ExchangeName,
            type: ExchangeType.Direct,
            durable: true
        );

        var message = SerializeMessage(@event);

        channel.BasicPublish(
            exchange: ExchangeName,
            routingKey: routingKey,
            mandatory: true,
            body: message
        );

        return Task.CompletedTask;
    }

    private byte[] SerializeMessage(IntegrationEvent @event)
    {
        return JsonSerializer.SerializeToUtf8Bytes(
            @event,
            @event.GetType(),
            _subscriptionInfo.JsonSerializerOptions
        );
    }
}