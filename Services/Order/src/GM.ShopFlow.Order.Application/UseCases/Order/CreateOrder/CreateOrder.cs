using GM.ShopFlow.Order.Application.Interfaces;
using GM.ShopFlow.Order.Domain.Entities;
using GM.ShopFlow.Order.Domain.SeedWork.Repositories;
using Entities = GM.ShopFlow.Order.Domain.Entities;

namespace GM.ShopFlow.Order.Application.UseCases.Order.CreateOrder;

public class CreateOrder(
    ICustomerRepository customerRepository,
    IOrderRepository orderRepository,
    IUnitOfWork unitOfWork
) : ICreateOrder
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly IOrderRepository _orderRepository = orderRepository;
    private IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(CreateOrderInput request, CancellationToken cancellationToken)
    {
        var orderItems = request.Items.Select(i =>
            new OrderItem(
                i.Quantity,
                i.ProductId,
                i.ProductName,
                i.ProductPrice
            )
        ).ToList();

        //Check if products and quantities is available in stock

        var user = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);

        //Check if user exists

        var order = new Entities.Order(
            orderItems,
            user,
            request.PercentageDiscount
        );

        await _orderRepository.CreateAsync(order, cancellationToken);

        await _unitOfWork.CommitAsync();

        // Throw an event CreatedOrder
    }
}
