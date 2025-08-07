namespace CapybaraPetApp.Api.Endpoints.Users;

public static class UserEndpointExtensions
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapCreateUser();
        app.MapGetUser();
        app.MapGetCapybaras();
        app.MapAdoptCapybara();
        app.MapUnlockAchievement();
        app.MapBuyItem();
        app.MapUseItem();

        return app;
    }
}