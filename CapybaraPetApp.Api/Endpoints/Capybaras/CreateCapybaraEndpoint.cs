using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Capybaras.Commands.CreateCapybara;
using CapybaraPetApp.Domain.CapybaraAggregate;
using ErrorOr;

namespace CapybaraPetApp.Api.Endpoints.Capybaras;

public static class CreateCapybaraEndpoint
{
    private const string Name = "CreateCapybara";

    public static IEndpointRouteBuilder MapCreateCapybara(this IEndpointRouteBuilder app)
    {
        app.MapPost(APIEndpoints.Capybara.Create, async (
                string capybaraName,
                ICommandHandler<CreateCapybaraCommand, ErrorOr<Capybara>> commandHandler) =>
            {
                var command = new CreateCapybaraCommand(capybaraName);

                var result = await commandHandler.Handle(command);

                return result.IsError
                    ? Results.BadRequest(result.Errors)
                    : TypedResults.CreatedAtRoute(result.Value, GetCapybaraEndpoint.Name, new { id = result.Value.Id });
            })
            .WithName(Name)
            .RequireAuthorization();

        return app;
    }
}