using MediatR;

namespace GM.ShopFlow.Product.Application.UseCases.Stock.SupplyStock;

public interface ISupplyStock : IRequestHandler<SupplyStockInput>
{

}
