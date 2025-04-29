using EventBus.Abstractions;
using EventBus.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace EventBusRabbitMQ;

public class RabbitMQEventBusConsumer(
    IConnection connection,
    IOptions<EventBusOptions> options,
    IOptions<EventBusSubscriptionInfo> subscriptionInfo,
    IServiceProvider serviceProvider
) : BackgroundService, IDisposable
{
    private readonly IConnection _connection = connection;
    private readonly string _queueName = options.Value.SubscriptionClientName;
    private readonly EventBusSubscriptionInfo _subscriptionInfo = subscriptionInfo.Value;
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private IModel _consumerChannel;

    private const string ExchangeName = "shopflow_exchange";


    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumerChannel = _connection.CreateModel() ??
             throw new InvalidOperationException("RabbitMQ connection is not open");

        _consumerChannel.ExchangeDeclare(
            exchange: ExchangeName,
            type: ExchangeType.Direct,
            durable: true
        );

        _consumerChannel.QueueDeclare(
            queue: _queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        var consumer = new AsyncEventingBasicConsumer(_consumerChannel);

        consumer.Received += OnMessageReceived;

        _consumerChannel.BasicConsume
        (
            queue: _queueName,
            autoAck: false,
            consumer: consumer
        );

        foreach (var (eventName, _) in _subscriptionInfo.EventTypes)
        {
            _consumerChannel.QueueBind
            (
                queue: _queueName,
                exchange: ExchangeName,
                routingKey: eventName
            );
        }

        return Task.CompletedTask;
    }

    private async Task OnMessageReceived(object sender, BasicDeliverEventArgs eventArgs)
    {
        if (!_subscriptionInfo.EventTypes.TryGetValue(eventArgs.RoutingKey, out var eventType))
            throw new InvalidOperationException($"Invalid event name {eventArgs.RoutingKey}");

        var body = Encoding.UTF8.GetString(eventArgs.Body.Span);

        var message = DeserializeMessage(body, eventType);

        await using var scope = _serviceProvider.CreateAsyncScope();

        var handlers = scope.ServiceProvider.GetKeyedServices<IIntegrationEventHandler>(eventType);

        foreach (var handler in handlers)
        {
            await handler.HandleAsync(message);
        }
    }

    private IntegrationEvent DeserializeMessage(string body, Type eventType)
    {
        return JsonSerializer.Deserialize(body, eventType) as IntegrationEvent;
    }

    public void Dispose()
    {
        _consumerChannel?.Dispose();
    }
}
