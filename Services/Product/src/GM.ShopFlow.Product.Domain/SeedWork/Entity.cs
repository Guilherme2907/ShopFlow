namespace GM.ShopFlow.Product.Domain.SeedWork;

public abstract class Entity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;

    public IList<DomainEvent> DomainEvents { get; private set; } = [];

    public void RaiseDomainEvent(DomainEvent domainEvent)
    {
        DomainEvents.Add(domainEvent);
    } 
    
    public void ClearDomainEvents()
    {
        DomainEvents.Clear();
    }
}
