using CapybaraPetApp.Api.Endpoints.Users.Requests;
using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Users.Commands.CreateUser;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;

namespace CapybaraPetApp.Api.Endpoints.Users;

public static class CreateUserEndpoint
{
    private const string Name = "CreateUser";

    public static IEndpointRouteBuilder MapCreateUser(this IEndpointRouteBuilder app)
    {
        app.MapPost(APIEndpoints.User.Create, async (
                RegisterUserRequest request, ICommandHandler<CreateUserCommand, ErrorOr<User>> commandHandler) =>
            {
                var command = new CreateUserCommand(request.Username, request.Email, request.Id);

                var result = await commandHandler.Handle(command);

                return result.IsError
                    ? EndpointsExtensions.Problem(result.Errors)
                    : TypedResults.CreatedAtRoute(result.Value, GetUserEndpoint.Name, new { id = result.Value.Id });
            })
            .WithName(Name)
            .RequireAuthorization();

        return app;
    }
}