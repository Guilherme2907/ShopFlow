using GM.ShopFlow.Order.Application.SettingModels;
using GM.ShopFlow.Order.Infra.ExternalServices.Apis;
using GM.ShopFlow.Order.Infra.ExternalServices.Interfaces;
using GM.ShopFlow.Order.Infra.ExternalServices.Models.User;
using Microsoft.Extensions.Options;

namespace GM.ShopFlow.Order.Infra.ExternalServices.Services;

public class AuthService(
    IAuthApi authApi,
    IOptions<AuthApiSettings> authApiSettings
) : IAuthService
{
    private readonly IAuthApi _authApi = authApi;
    private readonly AuthApiSettings _authApiSettings = authApiSettings.Value;

    public async Task<LoginUserResponse> LoginAdminAsync(CancellationToken cancellationToken)
    {
        var request = new LoginUserRequest(_authApiSettings.UserNameAdmin, _authApiSettings.PasswordAdmin);

        return await _authApi.LoginAsync(request, cancellationToken);
    }
}
