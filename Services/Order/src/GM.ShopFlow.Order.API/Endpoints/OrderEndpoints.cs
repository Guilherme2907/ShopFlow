using GM.ShopFlow.Order.Application.UseCases.Order.CreateOrder;
using GM.ShopFlow.Order.Application.UseCases.Order.GetOrders;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GM.ShopFlow.Order.API.Endpoints;

public static class OrderEndpoints
{
    public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder
            .MapGroup("api/orders")
            .WithTags("Orders")
            .WithOpenApi();

        group.MapPost("/", CreateOrder).RequireAuthorization(policy => policy.RequireRole("Customer"));

        group.MapGet("/{customerId}", GetOrders);

        return builder;
    }

    public static async Task<Ok> CreateOrder([FromBody] CreateOrderInput input, [FromServices] IMediator mediator)
    {
        await mediator.Send(input, CancellationToken.None);

        return TypedResults.Ok();
    } 
    
    public static async Task<Ok<IEnumerable<GetOrdersOutput>>> GetOrders(Guid customerId, [FromServices] IMediator mediator)
    {
        var result = await mediator.Send(new GetOrdersInput(customerId), CancellationToken.None);

        return TypedResults.Ok(result);
    }
}
