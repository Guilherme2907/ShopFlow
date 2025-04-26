using GM.ShopFlow.Product.Application.UseCases.Product.CreateProduct;
using GM.ShopFlow.Product.Application.UseCases.Product.GetProductById;
using GM.ShopFlow.Product.Application.UseCases.Product.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GM.ShopFlow.Product.Api.Endpoints;

public static class ProductEndpoints
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder
            .MapGroup("api/products")
            .WithTags("Products")
            .WithOpenApi();

        group.MapPost("/", CreateProduct);
        group.MapGet("/", GetProducts);
        group.MapGet("/{id}", GetProductById);

        return builder;
    }

    public static async Task<Ok> CreateProduct([FromBody] CreateProductInput input, [FromServices] IMediator mediator)
    {
        await mediator.Send(input, CancellationToken.None);

        return TypedResults.Ok();
    }

    public static async Task<Ok<List<GetProductsOutput>>> GetProducts([FromServices] IMediator mediator)
    {
        var result = await mediator.Send(new GetProductsInput(), CancellationToken.None);

        return TypedResults.Ok(result);
    }

    public static async Task<Ok<GetProductByIdOutput>> GetProductById(Guid id, [FromServices] IMediator mediator)
    {
        var result = await mediator.Send(new GetProductByIdInput(id), CancellationToken.None);

        return TypedResults.Ok(result);
    }
}
