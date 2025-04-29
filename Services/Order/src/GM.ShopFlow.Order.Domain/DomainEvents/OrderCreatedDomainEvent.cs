using GM.ShopFlow.Order.Domain.SeedWork;
using MediatR;

namespace GM.ShopFlow.Order.Domain.DomainEvents;

public class OrderCreatedDomainEvent(Entities.Order order) : DomainEvent, INotification
{
  public Entities.Order Order { get; private set; } = order;
}

