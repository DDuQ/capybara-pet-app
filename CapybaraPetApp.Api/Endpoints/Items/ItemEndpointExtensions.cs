namespace CapybaraPetApp.Api.Endpoints.Items;

public static class ItemEndpointExtensions
{
    public static IEndpointRouteBuilder MapItemEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGetItem();
        app.MapCreateItem();
        return app;
    }
}