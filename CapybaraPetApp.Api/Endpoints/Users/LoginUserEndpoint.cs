using CapybaraPetApp.Api.Endpoints.Users.Requests;
using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Dtos;
using CapybaraPetApp.Application.Users.Queries.LoginUser;
using ErrorOr;

namespace CapybaraPetApp.Api.Endpoints.Users;

public static class LoginUserEndpoint
{
    public static IEndpointRouteBuilder MapLoginUser(this IEndpointRouteBuilder app)
    {
        app.MapPost(APIEndpoints.User.Login, async (
            LoginUserRequest request, IQueryHandler<LoginUserQuery, ErrorOr<TokenResponseDto>> queryHandler, CancellationToken cancellationToken) =>
        {
            var query = new LoginUserQuery(request.UsernameOrEmail, request.Password);
            var result = await queryHandler.Handle(query, cancellationToken);
            
            return result.Match(
                Results.Ok,
                Results.BadRequest
            );
        });

        return app;
    }

    
}