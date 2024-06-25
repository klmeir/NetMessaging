using MediatR;
using NetMessaging.Api.Filters;
using NetMessaging.Application.Persons;
using NetMessaging.Domain.Dtos;

namespace NetMessaging.Api.ApiHandlers
{
    public static class PersonApi
    {
        public static RouteGroupBuilder MapTurn(this IEndpointRouteBuilder routeHandler)
        {
            routeHandler.MapPost("/", async (IMediator mediator, [Validate] PersonAddCommand person) =>
            {
                return Results.Ok(await mediator.Send(person));
            })
            .Produces(StatusCodes.Status200OK, typeof(ResponseDto))
            .Produces(StatusCodes.Status400BadRequest);

            return (RouteGroupBuilder)routeHandler;
        }
    }
}
