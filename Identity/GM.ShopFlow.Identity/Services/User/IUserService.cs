using GM.ShopFlow.Identity.Dtos;
using GM.ShopFlow.Identity.Dtos.Login;
using GM.ShopFlow.Identity.Dtos.User;

namespace GM.ShopFlow.Identity.Services.User;

public interface IUserService
{
    Task RegisterAsync(RegisterUserRequest request, CancellationToken ct = default);
}
