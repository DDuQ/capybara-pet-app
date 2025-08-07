using CapybaraPetApp.Api.Endpoints.Users.Responses;
using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Users.Queries.GetItems;
using CapybaraPetApp.Domain.Common.JoinTables;
using ErrorOr;

namespace CapybaraPetApp.Api.Endpoints.Users;

public static class GetItemsEndpoint
{
    public const string Name = "GetItems";

    public static IEndpointRouteBuilder MapGetItems(this IEndpointRouteBuilder app)
    {
        app.MapGet(APIEndpoints.User.GetItems,
            async (Guid userId, IQueryHandler<GetItemsQuery, ErrorOr<List<UserItem>>> queryHandler) =>
            {
                var query = new GetItemsQuery(userId);
                
                var result = await queryHandler.Handle(query);
                
                return result.IsError
                    ? EndpointsExtensions.Problem(result.Errors)
                    : TypedResults.Ok(result.Value.MapToResponse());
            })
            .WithName(Name)
            .RequireAuthorization();
        return app;
    }
}