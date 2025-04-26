using GM.ShopFlow.Order.Domain.Entities;

namespace GM.ShopFlow.Order.Domain.SeedWork.Repositories;

public interface ICustomerRepository
{
    Task CreateAsync(Customer customer, CancellationToken cancellationToken);

    Task<Customer> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
