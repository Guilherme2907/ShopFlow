using GM.ShopFlow.Order.Infra.ExternalServices.Models.User;
using RestEase;

namespace GM.ShopFlow.Order.Infra.ExternalServices.Apis;

public interface IAuthApi
{
    [Post("/login")]
    Task<LoginUserResponse> LoginAsync([Body] LoginUserRequest request, CancellationToken cancellationToken);
}
