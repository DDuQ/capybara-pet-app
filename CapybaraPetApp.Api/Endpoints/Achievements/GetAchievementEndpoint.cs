using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Achievements.Queries;
using CapybaraPetApp.Domain.AchievementAggregate;
using ErrorOr;

namespace CapybaraPetApp.Api.Endpoints.Achievements;

public static class GetAchievementEndpoint
{
    public const string Name = "GetAchievement";

    public static IEndpointRouteBuilder MapGetAchievement(this IEndpointRouteBuilder app)
    {
        app.MapGet(APIEndpoints.Achievements.Get, async (
                Guid id,
                IQueryHandler<GetAchievementQuery, ErrorOr<Achievement>> queryHandler) =>
            {
                var query = new GetAchievementQuery(id);

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