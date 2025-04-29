using GM.ShopFlow.Product.Application.Interfaces;
using GM.ShopFlow.Product.Domain.SeedWork;
using GM.ShopFlow.Product.Infra.Data.Context;
using MediatR;

namespace GM.ShopFlow.Product.Infra.Data;

public class UnitOfWork(
    ProductDbContext context,
    IMediator mediator
) : IUnitOfWork
{
    private readonly ProductDbContext _context = context;
    private readonly IMediator _mediator = mediator;


    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();

        await DispatchDomainEventsAsync(_mediator, _context);
    }

    private static async Task DispatchDomainEventsAsync(IMediator mediator, ProductDbContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}