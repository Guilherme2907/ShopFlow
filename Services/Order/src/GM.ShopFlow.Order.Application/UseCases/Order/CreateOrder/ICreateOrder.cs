using GM.ShopFlow.Order.Application.UseCases.Customer.CreateCustomer;
using MediatR;

namespace GM.ShopFlow.Order.Application.UseCases.Order.CreateOrder;

public interface ICreateOrder : IRequestHandler<CreateOrderInput>
{
}
