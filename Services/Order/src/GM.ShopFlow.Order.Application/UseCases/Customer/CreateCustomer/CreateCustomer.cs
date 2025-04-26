using GM.ShopFlow.Order.Application.Interfaces;
using GM.ShopFlow.Order.Domain.SeedWork.Repositories;
using GM.ShopFlow.Order.Domain.ValueObjects;
using Entities = GM.ShopFlow.Order.Domain.Entities;

namespace GM.ShopFlow.Order.Application.UseCases.Customer.CreateCustomer;

public class CreateCustomer(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork
) : ICreateCustomer
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Handle(CreateCustomerInput request, CancellationToken cancellationToken)
    {
        //Get email from token

        //Throw an event to add customer role to user

        var cpfOrCnpj = new CpfOrCnpj(request.CpfOrCnpj);
        var email = new Email(request.Email);

        var customer = new Entities.Customer(
            request.Name,
            cpfOrCnpj,
            email
        );

        await _customerRepository.CreateAsync(customer, cancellationToken);

        await _unitOfWork.CommitAsync();
    }
}
