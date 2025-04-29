using GM.ShopFlow.Order.Domain.Entities;
using GM.ShopFlow.Order.Domain.SeedWork;
using MediatR;

namespace GM.ShopFlow.Order.Domain.DomainEvents;

public class CustomerCreatedDomainEvent(Customer customer) : DomainEvent, INotification
{
    public Customer Customer { get; private set; } = customer;
}
