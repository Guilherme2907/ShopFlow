using MediatR;

namespace GM.ShopFlow.Order.Application.UseCases.Order.GetOrders;

public interface IGetOrders : IRequestHandler<GetOrdersInput, IEnumerable<GetOrdersOutput>>
{
}
