using Microsoft.AspNetCore.Identity;

namespace GM.ShopFlow.Identity.Models;

public class User : IdentityUser
{
    public string? RefreshToken { get; set; }

    public DateTime RefreshTokenExpiryTime { get; set; }
}
