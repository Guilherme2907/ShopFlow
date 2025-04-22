namespace GM.ShopFlow.Order.Domain.SeedWork;

public abstract class Entity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreatedDate { get; } = DateTime.UtcNow;

    public IList<DomainEvent> Events { get; private set; } = [];

    public void RaiseEvent(DomainEvent domainEvent)
    {
        Events.Add(domainEvent);
    }

    public void ClearEvents()
    {
        Events.Clear();
    }
}
