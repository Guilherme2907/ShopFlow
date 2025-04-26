using MediatR;

namespace GM.ShopFlow.Order.Application.UseCases.Customer.CreateCustomer;

public interface ICreateCustomer : IRequestHandler<CreateCustomerInput>
{
}
