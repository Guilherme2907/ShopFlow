using GM.ShopFlow.Product.Application.Interfaces;
using GM.ShopFlow.Product.Domain.SeedWork.Repositories;

namespace GM.ShopFlow.Product.Application.UseCases.Stock.SupplyStock;

public class SupplyStock(
    IStockRepository stockRepository,
    IUnitOfWork unitOfWork
) : ISupplyStock
{
    private readonly IStockRepository _stockRepository = stockRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(SupplyStockInput request, CancellationToken cancellationToken)
    {
        var stock = await _stockRepository.GetByProductIdAsync(request.ProductId, cancellationToken);

        stock.AddProductToStock(request.Quantity);

        await _stockRepository.UpdateAsync(stock, cancellationToken);

        //Throw an event StockUpdated

        await _unitOfWork.CommitAsync();
    }
}
