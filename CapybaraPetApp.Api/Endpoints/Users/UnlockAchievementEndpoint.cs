using CapybaraPetApp.Api.Endpoints.Users.Requests;
using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Users.Commands.AssignUserAchievement;
using ErrorOr;

namespace CapybaraPetApp.Api.Endpoints.Users;

public static class UnlockAchievementEndpoint
{
    public const string Name = "UnlockAchievement";

    public static IEndpointRouteBuilder MapUnlockAchievement(this IEndpointRouteBuilder app)
    {
        app.MapPost(APIEndpoints.User.UnlockAchievement, async (
                Guid id,
                UnlockAchievementRequest request,
                ICommandHandler<UnlockUserAchievementCommand, ErrorOr<Success>> commandHandler) =>
            {
                var command = new UnlockUserAchievementCommand(id, request.AchievementId);

                var assignUserAchievementResult = await commandHandler.Handle(command);

                return assignUserAchievementResult.IsError
                    ? EndpointsExtensions.Problem(assignUserAchievementResult.Errors)
                    : TypedResults.Ok(assignUserAchievementResult.Value);
            })
            .WithName(Name)
            .RequireAuthorization();

        return app;
    }
}