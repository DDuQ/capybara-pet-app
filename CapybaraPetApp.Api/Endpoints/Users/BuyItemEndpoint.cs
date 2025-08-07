using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Users.Commands.AssignItem;
using ErrorOr;

namespace CapybaraPetApp.Api.Endpoints.Users;

public static class BuyItemEndpoint
{
    public const string Name = "BuyItem";

    public static IEndpointRouteBuilder MapBuyItem(this IEndpointRouteBuilder app)
    {
        app.MapPost(APIEndpoints.User.BuyItem,
                async (Guid userId, Guid itemId, ICommandHandler<BuyItemCommand, ErrorOr<Success>> commandHandler) =>
                {
                    var command = new BuyItemCommand(userId, itemId);

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