namespace GM.ShopFlow.Product.Application.IntegrationsEvents.Events;

public record OrderStockItem(Guid ProductId, int Units);
