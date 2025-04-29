using GM.ShopFlow.Order.Application.Interfaces;
using GM.ShopFlow.Order.Domain.SeedWork;
using GM.ShopFlow.Order.Infra.Data.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GM.ShopFlow.Order.Infra.Data;

public class UnitOfWork(
    OrderDbContext context,
    IMediator mediator
) : IUnitOfWork
{
    private readonly OrderDbContext _context = context;
    private readonly IMediator _mediator = mediator;


    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();

        await DispatchDomainEventsAsync(_mediator, _context);
    }

    private static async Task DispatchDomainEventsAsync(IMediator mediator, OrderDbContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}


