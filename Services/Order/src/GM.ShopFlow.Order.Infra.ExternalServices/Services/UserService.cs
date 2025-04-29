using GM.ShopFlow.Order.Application.Interfaces;
using GM.ShopFlow.Order.Infra.ExternalServices.Apis;
using GM.ShopFlow.Order.Infra.ExternalServices.Interfaces;
using GM.ShopFlow.Order.Infra.ExternalServices.Models.Role;

namespace GM.ShopFlow.Order.Infra.ExternalServices.Services;

public class UserService(
    IAuthService authService,
    IUserApi userApi
) : IUserService
{
    private readonly IAuthService _authService = authService;
    private readonly IUserApi _userApi = userApi;

    public async Task AddRoleCustomerToUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        var loginResponse = await _authService.LoginAdminAsync(cancellationToken);

        _userApi.Authorization = $"{loginResponse.TokenType} {loginResponse.AccessToken}";

        await _userApi.AddRoleToUser(
            userId,
            new AddRoleRequest("Customer"),
            cancellationToken
        );

    }
}
