using GM.ShopFlow.Identity.Dtos.Login;
using GM.ShopFlow.Identity.Dtos.Refresh;
using GM.ShopFlow.Identity.Services.Token;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Model = GM.ShopFlow.Identity.Models;

namespace GM.ShopFlow.Identity.Services.Auth;

public class AuthService(
    SignInManager<Model.User> signInManager,
    UserManager<Model.User> userManager,
    ITokenService tokenService,
    IConfiguration configuration
)
    : IAuthService
{
    private readonly SignInManager<Model.User> _signInManager = signInManager;
    private readonly ITokenService _tokenService = tokenService;
    private readonly UserManager<Model.User> _userManager = userManager;
    private readonly IConfiguration _configuration = configuration;

    public async Task<LoginResponse> LoginAsync(LoginRequest request, CancellationToken ct)
    {
        var result = await _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);

        if (!result.Succeeded)
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        var user = await _userManager.FindByNameAsync(request.Username);

        var refreshToken = _tokenService.GenerateRefreshToken();

        await UpdateUserRefreshTokenAsync(user!, refreshToken);

        var claims = await _tokenService.GetUserClaims(user!);

        return new LoginResponse(
            _tokenService.GenerateAccessToken(claims),
            refreshToken,
            Int32.Parse(_configuration["JwtConfig:AccessTokenExpirationTime"]!),
            Int32.Parse(_configuration["JwtConfig:RefreshTokenExpirationTime"]!),
            "Bearer"
        );
    }

    public async Task<LoginResponse> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken ct)
    {
        var principal = _tokenService.GetPrincipalFromToken(request.AccessToken);
        var userName = principal.Identity?.Name ?? throw new ArgumentNullException("Invalid access token");

        var user = await _userManager.FindByNameAsync(userName);

        if (user is null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            throw new UnauthorizedAccessException("Invalid refresh token.");

        var refreshToken = _tokenService.GenerateRefreshToken();

        await UpdateUserRefreshTokenAsync(user!, refreshToken);

        return new LoginResponse(
           _tokenService.GenerateAccessToken(principal.Claims),
           refreshToken,
           Int32.Parse(_configuration["JwtConfig:AccessTokenExpirationTime"]!),
           Int32.Parse(_configuration["JwtConfig:RefreshTokenExpirationTime"]!),
           "Bearer"
       );
    }

    private async Task UpdateUserRefreshTokenAsync(Model.User user, string refreshToken)
    {
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddSeconds(
            Int32.Parse(_configuration["JwtConfig:RefreshTokenExpirationTime"]!)
        );

        await _userManager.UpdateAsync(user);
    }
}
