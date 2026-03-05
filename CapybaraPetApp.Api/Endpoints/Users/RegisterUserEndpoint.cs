using CapybaraPetApp.Api.Endpoints.Users.Requests;
using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Users.Commands.RegisterUser;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;

namespace CapybaraPetApp.Api.Endpoints.Users;

public static class RegisterUserEndpoint
{
    private const string Name = "RegisterUser";

    public static IEndpointRouteBuilder MapRegisterUser(this IEndpointRouteBuilder app)
    {
        app.MapPost(APIEndpoints.User.Register, async (
                RegisterUserRequest request, ICommandHandler<RegisterUserCommand, ErrorOr<User>> commandHandler) =>
            { 
                var command = new RegisterUserCommand(request.Username, request.Email, request.Password, request.Id);

                var result = await commandHandler.Handle(command);

                return result.IsError
                    ? EndpointsExtensions.Problem(result.Errors)
                    : TypedResults.CreatedAtRoute(result.Value, GetUserEndpoint.Name, new { id = result.Value.Id });
            })
            .WithName(Name);

        return app;
    }
}