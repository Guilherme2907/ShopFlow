using GM.ShopFlow.Order.Infra.ExternalServices.Models.User;

namespace GM.ShopFlow.Order.Infra.ExternalServices.Interfaces;

public interface IAuthService
{
    Task<LoginUserResponse> LoginAdminAsync(CancellationToken cancellationToken);
}
