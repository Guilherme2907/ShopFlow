using GM.ShopFlow.Identity.Dtos;
using GM.ShopFlow.Identity.Dtos.Refresh;
using GM.ShopFlow.Identity.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace GM.ShopFlow.Identity.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder
            .MapGroup("api/auth")
            .WithTags("Identity and Access")
            .WithOpenApi();

        group
            .MapLogin()
            .MapRefreshToken();

        return builder;
    }

    public static RouteGroupBuilder MapLogin(this RouteGroupBuilder builder)
    {
        builder.MapPost("/login", async ([FromBody] LoginRequest loginRequest, [FromServices] IAuthService authService) =>
        {
            return Results.Ok(await authService.LoginAsync(loginRequest));
        });

        return builder;
    }

    public static RouteGroupBuilder MapRefreshToken(this RouteGroupBuilder builder)
    {
        builder.MapPost("/refresh", async ([FromServices] IAuthService authService, [FromBody] RefreshTokenRequest request) =>
        {
            return Results.Ok(await authService.RefreshTokenAsync(request));
        });

        return builder;
    }
}
