using GM.ShopFlow.Identity.Dtos.User;
using Microsoft.AspNetCore.Identity;
using Model = GM.ShopFlow.Identity.Models;

namespace GM.ShopFlow.Identity.Services.User;

public class UserService(
    UserManager<Model.User> userManager
) : IUserService
{
    private readonly UserManager<Model.User> _userManager = userManager;

    public async Task RegisterAsync(RegisterUserRequest request, CancellationToken ct = default)
    {
        var user = new Model.User
        {
            UserName = request.UserName,
            PasswordHash = request.Password,
            Email = request.Email,
        };

        var result = await _userManager.CreateAsync(user, user.PasswordHash!);

        if (!result.Succeeded)
        {
            var errorMessage = string.Join(',',result.Errors.Select(e => e.Description));
            throw new Exception(errorMessage);
        }
    }
}
