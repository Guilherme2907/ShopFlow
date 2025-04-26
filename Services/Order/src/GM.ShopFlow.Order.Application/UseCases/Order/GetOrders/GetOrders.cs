
using GM.ShopFlow.Order.Domain.SeedWork.Repositories;

namespace GM.ShopFlow.Order.Application.UseCases.Order.GetOrders;

public class GetOrders(IOrderRepository orderRepository) : IGetOrders
{
    private readonly IOrderRepository _orderRepository = orderRepository;

    public async Task<IEnumerable<GetOrdersOutput>> Handle(GetOrdersInput request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetByCustomerIdAsync(request.CustomerId, cancellationToken);

        //Check if orders exists

        var order = orders.FirstOrDefault();

        return orders.Select(GetOrdersOutput.FromOrder);
    }
}
