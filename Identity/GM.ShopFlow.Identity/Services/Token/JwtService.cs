using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Model = GM.ShopFlow.Identity.Models;

namespace GM.ShopFlow.Identity.Services.Token;

public class JwtService(IConfiguration configuration, UserManager<Model.User> userManager)
    : ITokenService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly UserManager<Model.User> _userManager = userManager;

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:SymmetricSecurityKey"]!));

        var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddSeconds(Int32.Parse(_configuration["JwtConfig:AccessTokenExpirationTime"]!)),
            claims: claims,
            signingCredentials: signinCredentials,
            issuer: _configuration["JwtConfig:Issuer"]!,
            audience: _configuration["JwtConfig:Audience"]!
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public ClaimsPrincipal GetPrincipalFromToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidIssuer = _configuration["JwtConfig:Issuer"]!,
            ValidAudience = _configuration["JwtConfig:Audience"]!,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtConfig:SymmetricSecurityKey"]!)),
            ValidateLifetime = false
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

        if (securityToken is not JwtSecurityToken jwtToken ||
            !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }

    public async Task<List<Claim>> GetUserClaims(Model.User user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.NameIdentifier, user.Id),
            new("loginTimeStamp", DateTime.UtcNow.ToString())
        };

        claims.AddRange(userClaims);
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        return claims;
    }
}
