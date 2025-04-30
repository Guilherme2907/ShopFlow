using GM.ShopFlow.Product.Application.Interfaces;
using GM.ShopFlow.Product.Domain.Entities;
using GM.ShopFlow.Product.Domain.Events;
using GM.ShopFlow.Product.Domain.SeedWork.Repositories;
using MediatR;

namespace GM.ShopFlow.Product.Application.DomainEventHandlers;

public class ProductCreatedDomainEventHandler(
    IStockRepository stockRepository,
    IUnitOfWork unitOfWork
) : INotificationHandler<ProductCreatedDomainEvent>
{
    private readonly IStockRepository _stockRepository = stockRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;


    public async Task Handle(ProductCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var stock = new Stock(domainEvent.Product);

        await _stockRepository.CreateAsync(stock, cancellationToken);

        await _unitOfWork.CommitAsync();
    }
}
