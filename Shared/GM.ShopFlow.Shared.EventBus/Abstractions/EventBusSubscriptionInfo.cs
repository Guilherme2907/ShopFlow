using System.Text.Json.Serialization.Metadata;
using System.Text.Json;

namespace GM.ShopFlow.Shared.EventBus.Abstractions;

public class EventBusSubscriptionInfo
{
    public Dictionary<string, Type> EventTypes { get; } = [];

    public JsonSerializerOptions JsonSerializerOptions { get; } = new(DefaultSerializerOptions);

    internal static readonly JsonSerializerOptions DefaultSerializerOptions = new()
    {
        TypeInfoResolver = new DefaultJsonTypeInfoResolver()
    };
}
