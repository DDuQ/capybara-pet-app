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

                    return result.IsError
                        ? EndpointsExtensions.Problem(result.Errors)
                        : Results.NoContent();
                })
            .WithName(Name)
            .RequireAuthorization();

        return app;
    }
}