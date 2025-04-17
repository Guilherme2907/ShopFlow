using GM.ShopFlow.Identity.Dtos.Role;
using GM.ShopFlow.Identity.Dtos.User;
using GM.ShopFlow.Identity.Models;
using GM.ShopFlow.Identity.Services.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GM.ShopFlow.Identity.Endpoints;

public static class UserEndpoints
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder
            .MapGroup("api/users")
            .WithTags("Users")
            .WithOpenApi();

        group
            .MapRegisterUser()
            .MapAddRole();

        return builder;
    }

    public static RouteGroupBuilder MapRegisterUser(this RouteGroupBuilder builder)
    {
        builder.MapPost("/", async ([FromServices] IUserService userService, [FromBody] RegisterUserRequest request) =>
        {
            await userService.RegisterAsync(request);
        });

        return builder;
    }

    public static RouteGroupBuilder MapAddRole(this RouteGroupBuilder builder)
    {
        builder.MapPost("/{userName}/roles", async (string userName, [FromServices] UserManager<User> userManager, [FromBody] AddRoleRequest roleRequest) =>
        {
            var user = await userManager.FindByNameAsync(userName);

            await userManager.AddToRoleAsync(user!, roleRequest.Name);

            return Results.Ok();
        })
        .RequireAuthorization(policy => policy.RequireRole("admin"));

        return builder;
    }
}
