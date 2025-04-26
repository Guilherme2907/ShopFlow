namespace GM.ShopFlow.Order.Domain.SeedWork.Repositories;

public interface IOrderRepository
{
    Task CreateAsync(Entities.Order order, CancellationToken cancellationToken);

    Task<List<Entities.Order>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken);
}
