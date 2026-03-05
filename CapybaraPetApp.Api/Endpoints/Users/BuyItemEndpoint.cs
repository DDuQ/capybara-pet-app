using CapybaraPetApp.Api.Endpoints.Users.Requests;
using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Users.Commands.AssignItem;
using ErrorOr;

namespace CapybaraPetApp.Api.Endpoints.Users;

public static class BuyItemEndpoint
{
    public const string Name = "BuyItem";

    public static IEndpointRouteBuilder MapBuyItem(this IEndpointRouteBuilder app)
    {
        app.MapPost(APIEndpoints.User.BuyItem,
                async (Guid id, BuyItemRequest request, ICommandHandler<BuyItemCommand, ErrorOr<Success>> commandHandler) =>
                {
                    var command = new BuyItemCommand(id, request.ItemId);

                    var result = await commandHandler.Handle(command);

                    return result.Match(
                        _ => Results.NoContent(),
                        EndpointsExtensions.Problem
                    );
                })
            .WithName(Name)
            .RequireAuthorization();

        return app;
    }
}