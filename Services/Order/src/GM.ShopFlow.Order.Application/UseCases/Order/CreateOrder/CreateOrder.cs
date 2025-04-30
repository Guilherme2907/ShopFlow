using GM.ShopFlow.Order.Application.Interfaces;
using GM.ShopFlow.Order.Domain.Entities;
using GM.ShopFlow.Order.Domain.SeedWork.Repositories;
using Entities = GM.ShopFlow.Order.Domain.Entities;

namespace GM.ShopFlow.Order.Application.UseCases.Order.CreateOrder;

public class CreateOrder(
    ICustomerRepository customerRepository,
    IOrderRepository orderRepository,
    IProductStockRepository productStockRepository,
    IUnitOfWork unitOfWork,
    IProductStockService productStockService
) : ICreateOrder
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly IOrderRepository _orderRepository = orderRepository;
    private readonly IProductStockRepository _productStockRepository = productStockRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IProductStockService _productStockService = productStockService;

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

        var customer = await _customerRepository.GetByIdAsync(request.CustomerId, cancellationToken) ??
            throw new Exception("Customer is invalid");

        await ValidateItemsAvailabilityAsync(orderItems, cancellationToken);

        var order = new Entities.Order(
            orderItems,
            customer,
            request.PercentageDiscount
        );

        await _orderRepository.CreateAsync(order, cancellationToken);

        await _unitOfWork.CommitAsync();
    }

    private async Task ValidateItemsAvailabilityAsync(List<OrderItem> orderItems, CancellationToken cancellationToken)
    {
        if (!await _productStockRepository.HasProductStocksAsync(cancellationToken))
        {
            await _productStockService.PopulateProductStocksDbAsync(cancellationToken);
        }

        orderItems.ForEach(async i =>
        {
            var quantity = await _productStockRepository.GetStockAsync(i.ProductId.ToString()) ??
                throw new Exception($"Product {i.ProductId} doesn't exist");

            if (quantity < i.Quantity)
                throw new Exception($"Product {i.ProductName} doesn't have available quantity in stock");
        });
    }
}
