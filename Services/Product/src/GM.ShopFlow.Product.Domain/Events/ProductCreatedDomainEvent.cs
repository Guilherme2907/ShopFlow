using GM.ShopFlow.Product.Domain.SeedWork;
using MediatR;

namespace GM.ShopFlow.Product.Domain.Events;

public class ProductCreatedDomainEvent(Entities.Product product) : DomainEvent, INotification
{
    public readonly Entities.Product Product = product;
}
