using GM.ShopFlow.Shared.EventBus.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GM.ShopFlow.Shared.EventBusRabbitMQ;

public static class RabbitMqDependencyInjectionExtensions
{
    private const string SectionName = "EventBus";

    public static IEventBusBuilder AddRabbitMqEventBus(this IHostApplicationBuilder builder, string connectionName)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.AddRabbitMQClient(connectionName, configureConnectionFactory: factory =>
        {
            factory.DispatchConsumersAsync = true;
            factory.Port = 5672;
            factory.UserName = "gui";
            factory.Password = "123";
        });

        // Options support
        builder.Services.Configure<EventBusOptions>(builder.Configuration.GetSection(SectionName));

        // Abstractions on top of the core client API
        builder.Services.AddSingleton<IEventBus, RabbitMQEventBusProducer>();

        // Start consuming messages as soon as the application starts
        builder.Services.AddHostedService<RabbitMQEventBusConsumer>();

        return new EventBusBuilder(builder.Services);
    }

    private class EventBusBuilder(IServiceCollection services) : IEventBusBuilder
    {
        public IServiceCollection Services => services;
    }
}