using MediatR;

namespace GM.ShopFlow.Product.Application.UseCases.Stock.SupplyStock;

public record SupplyStockInput(Guid ProductId, int Quantity) : IRequest;
