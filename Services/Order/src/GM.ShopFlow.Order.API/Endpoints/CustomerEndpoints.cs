using GM.ShopFlow.Order.Application.UseCases.Customer.CreateCustomer;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace GM.ShopFlow.Order.API.Endpoints;

public static class CustomerEndpoints
{
    public static IEndpointRouteBuilder MapCustomerEndpoints(this IEndpointRouteBuilder builder)
    {
        var group = builder
            .MapGroup("api/customers")
            .WithTags("Customers")
            .WithOpenApi();

        group.MapPost("/", CreateCustomer);

        return builder;
    }

    public static async Task<Ok> CreateCustomer(
        [FromBody] CreateCustomerInput input,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken
    )
    {
        await mediator.Send(input, cancellationToken);

        return TypedResults.Ok();
    }
}
