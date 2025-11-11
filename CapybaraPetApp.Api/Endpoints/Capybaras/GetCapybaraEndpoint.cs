using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Capybaras.Queries;
using CapybaraPetApp.Domain.CapybaraAggregate;
using ErrorOr;

namespace CapybaraPetApp.Api.Endpoints.Capybaras;

public static class GetCapybaraEndpoint
{
    public const string Name = "GetCapybara";

    public static IEndpointRouteBuilder MapGetCapybara(this IEndpointRouteBuilder app)
    {
        app.MapGet(APIEndpoints.Capybara.Get, async (
                Guid id,
                IQueryHandler<GetCapybaraQuery, ErrorOr<Capybara>> queryHandler) =>
            {
                var query = new GetCapybaraQuery(id);

                var getCapybaraResult = await queryHandler.Handle(query);

                return getCapybaraResult.IsError
                    ? EndpointsExtensions.Problem(getCapybaraResult.Errors)
                    : Results.Ok(getCapybaraResult.Value);
            })
            .WithName(Name);

        return app;
    }
}