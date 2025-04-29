using MediatR;

namespace GM.ShopFlow.Order.Application.UseCases.Customer.CreateCustomer;

public record CreateCustomerInput(
    string Name,
    string CpfOrCnpj
) : IRequest;
