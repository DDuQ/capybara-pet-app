using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Users.Queries.GetUser;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;

namespace CapybaraPetApp.Api.Endpoints.Users;

public static class GetUserEndpoint
{
    public const string Name = "GetUser";

    public static IEndpointRouteBuilder MapGetUser(this IEndpointRouteBuilder app)
    {
        app.MapGet(APIEndpoints.User.Get, async (
                Guid id, IQueryHandler<GetUserQuery, ErrorOr<User>> queryHandler) =>
            {
                var query = new GetUserQuery(id);

                var result = await queryHandler.Handle(query);

                return result.IsError
                    ? EndpointsExtensions.Problem(result.Errors)
                    : TypedResults.Ok(result.Value);
            })
            .WithName(Name);

        return app;
    }
}