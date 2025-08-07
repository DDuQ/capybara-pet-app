using CapybaraPetApp.Api.Endpoints.Achievements;
using CapybaraPetApp.Api.Endpoints.Capybaras;
using CapybaraPetApp.Api.Endpoints.Items;
using CapybaraPetApp.Api.Endpoints.Users;
using ErrorOr;

namespace CapybaraPetApp.Api.Endpoints;

public static class EndpointsExtensions
{
    public static IEndpointRouteBuilder MapEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapUserEndpoints();
        app.MapCapybaraEndpoints();
        app.MapItemEndpoints();
        app.MapAchievementEndpoints();
        return app;
    }

    public static IResult Problem(List<Error> errors)
    {
        if (errors.Count is 0) return Results.Problem();

        return errors.All(error => error.Type == ErrorType.Validation) ? ValidationProblem(errors) : Problem(errors[0]);
    }

    private static IResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        return Results.Problem(statusCode: statusCode, title: error.Description);
    }

    private static IResult ValidationProblem(List<Error> errors)
    {
        var dictionary =
            errors.ToDictionary<Error, string, string[]>(error => error.Code, error => [error.Description]);

        return Results.ValidationProblem(dictionary);
    }
}