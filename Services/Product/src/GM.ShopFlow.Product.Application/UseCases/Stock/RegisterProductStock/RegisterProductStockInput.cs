using MediatR;

namespace GM.ShopFlow.Product.Application.UseCases.Stock.RegisterProductStock;

public record RegisterProductStockInput(Guid ProductId) : IRequest;
