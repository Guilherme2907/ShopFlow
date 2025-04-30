using Microsoft.Extensions.DependencyInjection;

namespace GM.ShopFlow.Shared.EventBus.Abstractions;
public interface IEventBusBuilder
{
    public IServiceCollection Services { get; }
}
