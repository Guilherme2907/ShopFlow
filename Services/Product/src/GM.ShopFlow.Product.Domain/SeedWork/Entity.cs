namespace GM.ShopFlow.Product.Domain.SeedWork;

public abstract class Entity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public DateTime CreatedDate { get; private set; } = DateTime.UtcNow;

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
