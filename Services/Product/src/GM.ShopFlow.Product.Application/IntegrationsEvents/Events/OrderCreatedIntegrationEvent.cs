﻿using GM.ShopFlow.Shared.EventBus.Events;

namespace GM.ShopFlow.Product.Application.IntegrationsEvents.Events;

public class OrderCreatedIntegrationEvent
(
    Guid orderId,
    Guid customerId,
    IEnumerable<OrderStockItem> items
) : IntegrationEvent
{
    public Guid OrderId { get; private set; } = orderId;
    public Guid CustomerId { get; private set; } = customerId;
    public IEnumerable<OrderStockItem> Items { get; private set; } = items;
}
