using GM.ShopFlow.Order.Domain.Events;
using GM.ShopFlow.Order.Domain.SeedWork;
using GM.ShopFlow.Order.Domain.ValueObjects;

namespace GM.ShopFlow.Order.Domain.Entities;

public class Customer : Entity
{
    public string Name { get; private set; }

    public CpfOrCnpj CpfOrCnpj { get; private set; }

    public Email Email { get; private set; }

    private readonly IList<Order> _orders = [];

    public IReadOnlyList<Order> Orders => _orders.AsReadOnly();

    public Customer(string name, CpfOrCnpj cpfOrCnpj, Email email)
    {
        Name = name;
        CpfOrCnpj = cpfOrCnpj;
        Email = email;

        RaiseEvent(new CustomerCreatedDomainEvent());
    }

    private Customer() { }

    public void Update(string? name = null, Email? email = null)
    {
        Name = name ?? Name;
        Email = email ?? Email;
    }
}
