using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Users.Queries.GetCapybaras;
using CapybaraPetApp.Domain.CapybaraAggregate;
using ErrorOr;

namespace CapybaraPetApp.Api.Endpoints.Capybaras;

public static class GetCapybaraEndpoint
{
    public const string Name = "GetCapybara";

    public static IEndpointRouteBuilder MapGetCapybaras(this IEndpointRouteBuilder app)
    {
        app.MapGet(APIEndpoints.Capybara.Get, async (
                Guid id,
                IQueryHandler<GetCapybarasQuery, ErrorOr<List<Capybara>>> queryHandler) =>
            {
                var query = new GetCapybarasQuery(id);

                var getCapybaraResult = await queryHandler.Handle(query);

                return getCapybaraResult.IsError
                    ? EndpointsExtensions.Problem(getCapybaraResult.Errors)
                    : Results.Ok(getCapybaraResult.Value);
            })
            .WithName(Name);

        return app;
    }
}