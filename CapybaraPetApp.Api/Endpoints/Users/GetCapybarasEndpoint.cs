using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Users.Queries.GetCapybaras;
using CapybaraPetApp.Domain.CapybaraAggregate;
using ErrorOr;

namespace CapybaraPetApp.Api.Endpoints.Users;

public static class GetCapybarasEndpoint
{
    private const string Name = "GetCapybaras";

    public static IEndpointRouteBuilder MapGetCapybaras(this IEndpointRouteBuilder app)
    {
        app.MapGet(APIEndpoints.User.GetCapybaras, async (
                Guid id, IQueryHandler<GetCapybarasQuery, ErrorOr<List<Capybara>>> queryHandler) =>
            {
                var query = new GetCapybarasQuery(id);

                var getCapybarasResult = await queryHandler.Handle(query);

                return getCapybarasResult.IsError
                    ? EndpointsExtensions.Problem(getCapybarasResult.Errors)
                    : TypedResults.Ok(getCapybarasResult.Value);
            })
            .WithName(Name);

        return app;
    }
}