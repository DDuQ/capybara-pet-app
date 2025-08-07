namespace CapybaraPetApp.Api.Endpoints.Capybaras;

public static class CapybaraEndpointExtensions
{
    public static IEndpointRouteBuilder MapCapybaraEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapCreateCapybara();
        app.MapGetCapybaras();
        return app;
    }
}