using GM.ShopFlow.Product.Application.Interfaces;
using GM.ShopFlow.Product.Domain.SeedWork.Repositories;
using Entities = GM.ShopFlow.Product.Domain.Entities;

namespace GM.ShopFlow.Product.Application.UseCases.Stock.RegisterProductStock;

public class RegisterProductStock(
    IStockRepository stockRepository,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork
) : IRegisterProductStock
{
    private readonly IStockRepository _stockRepository = stockRepository;
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(RegisterProductStockInput request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);

        //Check if product is null

        var stock = new Entities.Stock(product);

        await _stockRepository.CreateAsync(stock, cancellationToken);

        await _unitOfWork.CommitAsync();
    }
}
