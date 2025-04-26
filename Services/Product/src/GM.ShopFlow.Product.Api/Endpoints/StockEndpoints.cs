using GM.ShopFlow.Product.Application.UseCases.Product.CreateProduct;
using GM.ShopFlow.Product.Application.UseCases.Product.GetProductById;
using GM.ShopFlow.Product.Application.UseCases.Product.GetProducts;
using GM.ShopFlow.Product.Application.UseCases.Stock.RegisterProductStock;
using GM.ShopFlow.Product.Application.UseCases.Stock.SupplyStock;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GM.ShopFlow.Product.Api.Endpoints;

public static class StockEndpoints
{
    public static IEndpointRouteBuilder MapStockEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder
            .MapGroup("api/stocks")
            .WithTags("Stocks")
            .WithOpenApi();

        group.MapPost("/", RegisterStockProduct);
        group.MapPut("/", SupplyStock);

        return builder;
    }

    public static async Task<Ok> RegisterStockProduct([FromBody] RegisterProductStockInput input, [FromServices] IMediator mediator)
    {
        await mediator.Send(input, CancellationToken.None);

        return TypedResults.Ok();
    }

    public static async Task<Ok> SupplyStock([FromBody] SupplyStockInput input, [FromServices] IMediator mediator)
    {
        await mediator.Send(input, CancellationToken.None);

        return TypedResults.Ok();
    }
}
