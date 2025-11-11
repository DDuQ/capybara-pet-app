using CapybaraPetApp.Api.Endpoints.Users.Requests;
using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Users.Commands.UseItem;
using ErrorOr;

namespace CapybaraPetApp.Api.Endpoints.Users;

public static class UseItemEndpoint
{
    private const string Name = "UseItem";

    public static IEndpointRouteBuilder MapUseItem(this IEndpointRouteBuilder app)
    {
        app.MapPut(APIEndpoints.User.UseItem, async (
                Guid id,
                UseItemRequest request,
                ICommandHandler<UseItemCommand, ErrorOr<Success>> queryHandler) =>
            {
                var query = new UseItemCommand(id, request.CapybaraId, request.ItemId, request.ItemAmount);
                var useItemResult = await queryHandler.Handle(query);
                return useItemResult.IsError
                    ? EndpointsExtensions.Problem(useItemResult.Errors)
                    : TypedResults.Ok(useItemResult.Value);
            })
            .WithName(Name)
            .RequireAuthorization();

        return app;
    }
}