namespace GM.ShopFlow.Order.Domain.SeedWork;

public abstract class Entity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;

    public IList<DomainEvent> DomainEvents { get; private set; } = [];

    public void RaiseEvent(DomainEvent domainEvent)
    {
        DomainEvents.Add(domainEvent);
    }

    public void ClearEvents()
    {
        DomainEvents.Clear();
    }
}
