using System.Security.Claims;
using Model = GM.ShopFlow.Identity.Models;

namespace GM.ShopFlow.Identity.Services.Token;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);

    string GenerateRefreshToken();

    ClaimsPrincipal GetPrincipalFromToken(string token);

    Task<List<Claim>> GetUserClaims(Model.User user);
}
