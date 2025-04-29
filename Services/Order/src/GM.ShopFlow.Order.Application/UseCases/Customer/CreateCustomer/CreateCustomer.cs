using GM.ShopFlow.Order.Application.Interfaces;
using GM.ShopFlow.Order.Domain.SeedWork.Repositories;
using GM.ShopFlow.Order.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Entities = GM.ShopFlow.Order.Domain.Entities;

namespace GM.ShopFlow.Order.Application.UseCases.Customer.CreateCustomer;

public class CreateCustomer(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork,
    IHttpContextAccessor contextAccessor
) : ICreateCustomer
{
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

    public async Task Handle(CreateCustomerInput request, CancellationToken cancellationToken)
    {
        //Get email from token
        var loggedUserId = Guid.Parse(_contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var loggedUserEmail = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;

        //Throw an event to add customer role to user

        var cpfOrCnpj = new CpfOrCnpj(request.CpfOrCnpj);
        var email = new Email(loggedUserEmail!);

        var customer = new Entities.Customer(
            loggedUserId,
            request.Name,
            cpfOrCnpj,
            email
        );

        await _customerRepository.CreateAsync(customer, cancellationToken);

        await _unitOfWork.CommitAsync();
    }
}
