namespace GM.ShopFlow.Order.Application.IntegrationsEvents.Events;

public record OrderStockItem(Guid ProductId, int Units);
