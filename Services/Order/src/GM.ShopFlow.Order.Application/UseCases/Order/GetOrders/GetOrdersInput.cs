using MediatR;

namespace GM.ShopFlow.Order.Application.UseCases.Order.GetOrders;

public record GetOrdersInput(
    Guid CustomerId
) : IRequest<IEnumerable<GetOrdersOutput>>;

