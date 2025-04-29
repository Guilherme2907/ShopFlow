using GM.ShopFlow.Order.Infra.ExternalServices.Models.Role;
using RestEase;

namespace GM.ShopFlow.Order.Infra.ExternalServices.Apis;

public interface IUserApi
{
    [Header("Authorization")]
    public string Authorization { get; set; }

    [Post("/{userId}/roles")]
    Task AddRoleToUser(
        [Path] Guid userId,
        [Body] AddRoleRequest request,
        CancellationToken cancellationToken
    );
}
