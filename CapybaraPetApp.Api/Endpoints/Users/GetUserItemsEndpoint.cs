using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Dtos;
using CapybaraPetApp.Application.Users.Queries.GetItems;
using ErrorOr;

namespace CapybaraPetApp.Api.Endpoints.Users;

public static class GetUserItemsEndpoint
{
    public const string Name = "GetItems";

    public static IEndpointRouteBuilder MapGetItems(this IEndpointRouteBuilder app)
    {
        app.MapGet(APIEndpoints.User.GetItems,
                async (Guid id, IQueryHandler<GetUserItemsQuery, ErrorOr<List<InventoryItemDto>>> queryHandler) =>
                {
                    var query = new GetUserItemsQuery(id);

                    var result = await queryHandler.Handle(query);

                    return result.IsError
                        ? EndpointsExtensions.Problem(result.Errors)
                        : TypedResults.Ok(result.Value);
                })
            .WithName(Name)
            .RequireAuthorization();
        return app;
    }
}