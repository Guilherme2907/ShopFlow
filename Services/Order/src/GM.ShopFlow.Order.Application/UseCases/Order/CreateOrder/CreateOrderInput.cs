using MediatR;

namespace GM.ShopFlow.Order.Application.UseCases.Order.CreateOrder;

public record CreateOrderInput(
    float PercentageDiscount,
    Guid CustomerId,
    List<CreateOrderItemInput> Items
) : IRequest;
