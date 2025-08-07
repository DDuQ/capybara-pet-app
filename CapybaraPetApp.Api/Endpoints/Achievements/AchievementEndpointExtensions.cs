namespace CapybaraPetApp.Api.Endpoints.Achievements;

public static class AchievementEndpointExtensions
{
    public static IEndpointRouteBuilder MapAchievementEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapCreateAchievement();
        app.MapGetAchievement();
        return app;
    }
}