namespace GM.ShopFlow.Order.Application.Interfaces;

public interface IUserService
{
    Task AddRoleCustomerToUserAsync(Guid userId, CancellationToken cancellationToken = default);
}
