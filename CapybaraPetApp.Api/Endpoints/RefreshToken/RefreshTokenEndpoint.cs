using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Dtos;
using CapybaraPetApp.Application.Auth;
using ErrorOr;

namespace CapybaraPetApp.Api.Endpoints.RefreshToken;

public static class RefreshTokenEndpoint
{
    public static IEndpointRouteBuilder MapRefreshTokenEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost(APIEndpoints.RefreshToken.Refresh,
            async (RefreshTokenRequest request, IQueryHandler<RefreshTokenQuery, ErrorOr<TokenResponseDto>> queryHandler, CancellationToken cancellationToken) =>
            {
                var result = await queryHandler.Handle(new RefreshTokenQuery(request.UserId, request.RefreshToken), cancellationToken);
                
                return result.Match(
                    Results.Ok,
                    EndpointsExtensions.Problem
                );
            });
        
        return app;
    }
}