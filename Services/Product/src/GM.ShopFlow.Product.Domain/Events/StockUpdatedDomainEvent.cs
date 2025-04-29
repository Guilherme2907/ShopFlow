using GM.ShopFlow.Product.Domain.Entities;
using GM.ShopFlow.Product.Domain.SeedWork;
using MediatR;

namespace GM.ShopFlow.Product.Domain.Events;

public class StockUpdatedDomainEvent(Stock stock) : DomainEvent, INotification
{
    public Stock Stock { get; private set; } = stock;
}
