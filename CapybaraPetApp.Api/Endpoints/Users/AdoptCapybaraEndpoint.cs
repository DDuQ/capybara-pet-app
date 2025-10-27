using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Users.Commands.AdoptCapybara;
using ErrorOr;

namespace CapybaraPetApp.Api.Endpoints.Users;

public static class AdoptCapybaraEndpoint
{
    private const string Name = "AdoptCapybara";

    public static IEndpointRouteBuilder MapAdoptCapybara(this IEndpointRouteBuilder app)
    {
        app.MapPost(APIEndpoints.User.AdoptCapybara, async (
                Guid userId, Guid capybaraId, ICommandHandler<AdoptCapybaraCommand, ErrorOr<Success>> commandHandler) =>
            {
                var command = new AdoptCapybaraCommand(userId, capybaraId);

                var assignCapybaraResult = await commandHandler.Handle(command);

                return assignCapybaraResult.IsError
                    ? EndpointsExtensions.Problem(assignCapybaraResult.Errors)
                    : TypedResults.Ok(assignCapybaraResult.Value);
            })
            .WithName(Name)
            .RequireAuthorization();

        return app;
    }
}