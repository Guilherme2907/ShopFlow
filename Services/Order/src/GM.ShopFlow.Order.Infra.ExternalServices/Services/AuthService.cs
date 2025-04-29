using GM.ShopFlow.Order.Infra.ExternalServices.Apis;
using GM.ShopFlow.Order.Infra.ExternalServices.Interfaces;
using GM.ShopFlow.Order.Infra.ExternalServices.Models.User;

namespace GM.ShopFlow.Order.Infra.ExternalServices.Services;

public class AuthService(IAuthApi authApi) : IAuthService
{
    private readonly IAuthApi _authApi = authApi;

    public async Task<LoginUserResponse> LoginAdminAsync(CancellationToken cancellationToken)
    {
        var request = new LoginUserRequest("GuiAdmin", "Gui123!");

        return await _authApi.LoginAsync(request, cancellationToken);
    }
}
