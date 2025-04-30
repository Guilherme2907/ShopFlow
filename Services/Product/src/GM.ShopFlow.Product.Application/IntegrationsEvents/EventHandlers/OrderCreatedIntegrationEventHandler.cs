using GM.ShopFlow.Product.Application.IntegrationsEvents.Events;
using GM.ShopFlow.Product.Application.Interfaces;
using GM.ShopFlow.Product.Domain.SeedWork.Repositories;
using GM.ShopFlow.Shared.EventBus.Abstractions;

namespace GM.ShopFlow.Product.Application.IntegrationsEvents.EventHandlers;

public class OrderCreatedIntegrationEventHandler(
    IStockRepository stockRepository,
    IUnitOfWork unitOfWork
)
    : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
{
    private readonly IStockRepository _stockRepository = stockRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task HandleAsync(OrderCreatedIntegrationEvent @event)
    {
        var productIds = @event.Items.Select(i => i.ProductId);

        var stocks = await _stockRepository.GetByProductIdsAsync(productIds, CancellationToken.None);

        stocks.ToList().ForEach(stock =>
        {
            var units = @event.Items.FirstOrDefault(i => i.ProductId == stock.ProductId)!.Units;

            stock.RemoveProductFromStock(units);
        });

        await _stockRepository.UpdateAsync(stocks);

        await _unitOfWork.CommitAsync();
    }
}
