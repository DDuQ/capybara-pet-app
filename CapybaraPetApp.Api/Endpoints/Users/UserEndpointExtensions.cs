namespace CapybaraPetApp.Api.Endpoints.Users;

public static class UserEndpointExtensions
{
    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapRegisterUser();
        app.MapGetUser();
        app.MapGetCapybaras();
        app.MapAdoptCapybara();
        app.MapUnlockAchievement();
        app.MapBuyItem();
        app.MapUseItem();
        app.MapLoginUser();
        app.MapGetItems();
        
        return app;
    }
}