using GM.ShopFlow.Identity.Dtos.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GM.ShopFlow.Identity.Endpoints;

public static class RoleEndpoints
{
    public static IEndpointRouteBuilder MapRoleEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder
            .MapGroup("api/roles")
            .WithTags("Roles")
            .WithOpenApi();

        group
            .MapRegisterRole();

        return builder;
    }

    public static RouteGroupBuilder MapRegisterRole(this RouteGroupBuilder builder)
    {
        builder.MapPost("/", async ([FromServices] RoleManager<IdentityRole> roleManager, [FromBody] AddRoleRequest roleRequest) =>
        {
            await roleManager.CreateAsync(new IdentityRole(roleRequest.Name));

            return Results.Ok();
        })
        .RequireAuthorization(policy => policy.RequireRole("admin"));

        return builder;
    }
}
