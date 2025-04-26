using GM.ShopFlow.Product.Application.UseCases.Category.CreateCategory;
using GM.ShopFlow.Product.Application.UseCases.Category.GetCategories;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GM.ShopFlow.Product.Api.Endpoints;

public static class CategoryEndpoints
{
    public static IEndpointRouteBuilder MapCategoryEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder
            .MapGroup("api/categories")
            .WithTags("Categories")
            .WithOpenApi();

        group.MapPost("/", CreateCategory);
        group.MapGet("/", GetCategories);

        return builder;
    }

    public static async Task<Ok> CreateCategory([FromBody] CreateCategoryInput input, [FromServices] IMediator mediator)
    {
        await mediator.Send(input, CancellationToken.None);

        return TypedResults.Ok();
    }

    public static async Task<Ok<List<GetCategoriesOutput>>> GetCategories([FromServices] IMediator mediator)
    {
        var result = await mediator.Send(new GetCategoriesInput(), CancellationToken.None);

        return TypedResults.Ok(result);
    }
}
