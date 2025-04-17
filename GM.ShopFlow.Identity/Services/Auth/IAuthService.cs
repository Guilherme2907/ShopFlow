using GM.ShopFlow.Identity.Dtos;
using GM.ShopFlow.Identity.Dtos.Login;
using GM.ShopFlow.Identity.Dtos.Refresh;

namespace GM.ShopFlow.Identity.Services.Auth;

public interface IAuthService
{
    Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken ct = default);

    Task<LoginResponse> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken ct = default);
}
