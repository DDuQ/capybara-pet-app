using CapybaraPetApp.Api.Endpoints.Achievements.Requests;
using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Achievements.Commands;
using CapybaraPetApp.Domain.AchievementAggregate;
using ErrorOr;

namespace CapybaraPetApp.Api.Endpoints.Achievements;

public static class CreateAchievementEndpoint
{
    public const string Name = "CreateAchievement";

    public static IEndpointRouteBuilder MapCreateAchievement(this IEndpointRouteBuilder app)
    {
        app.MapPost(APIEndpoints.Achievements.Create, async (
                CreateAchievementRequest createAchievementRequest,
                ICommandHandler<CreateAchievementCommand, ErrorOr<Achievement>> commandHandler) =>
            {
                var command = new CreateAchievementCommand(
                    createAchievementRequest.Title,
                    createAchievementRequest.Description,
                    createAchievementRequest.Points,
                    createAchievementRequest.Rarity);

                var createAchievementResult = await commandHandler.Handle(command);

                return createAchievementResult.IsError
                    ? EndpointsExtensions.Problem(createAchievementResult.Errors)
                    : TypedResults.CreatedAtRoute(createAchievementResult.Value,
                        GetAchievementEndpoint.Name, new { id = createAchievementResult.Value.Id });
            })
            .WithName(Name)
            .RequireAuthorization();

        return app;
    }
}