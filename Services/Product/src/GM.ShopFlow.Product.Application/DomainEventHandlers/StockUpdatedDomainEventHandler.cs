using GM.ShopFlow.Product.Application.IntegrationsEvents.Events;
using GM.ShopFlow.Product.Application.Interfaces;
using GM.ShopFlow.Product.Domain.Events;
using GM.ShopFlow.Product.Domain.SeedWork.Repositories;
using GM.ShopFlow.Shared.EventBus.Abstractions;
using MediatR;

namespace GM.ShopFlow.Product.Application.DomainEventHandlers;

public class StockUpdatedDomainEventHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork,
    IEventBus eventBus
) : INotificationHandler<StockUpdatedDomainEvent>
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IEventBus _eventBus = eventBus;

    public async Task Handle(StockUpdatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(domainEvent.Stock.ProductId);

        product.SetQuantity(domainEvent.Stock.Quantity);

        await _productRepository.UpdateAsync(product, cancellationToken);

        await _eventBus.PublishAsync(
            new StockUpdatedIntegrationEvent(domainEvent.Stock.ProductId, domainEvent.Stock.Quantity),
            cancellationToken
        );

        await _unitOfWork.CommitAsync();
    }
}
