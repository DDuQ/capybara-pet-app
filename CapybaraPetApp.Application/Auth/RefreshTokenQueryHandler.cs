using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Dtos;
using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Auth;

public class RefreshTokenQueryHandler(IAuthService authService, IUserRepository userRepository, IAuthTokenRepository authRepository)
    : IQueryHandler<RefreshTokenQuery, ErrorOr<TokenResponseDto>>
{
    public async Task<ErrorOr<TokenResponseDto>> Handle(RefreshTokenQuery query, CancellationToken cancellationToken = default)
    {
        var token = await authService.IssueNewRefreshToken(query.UserId, query.RefreshToken);
        
        if (token is null)
        {
            return Error.Validation("Invalid refresh token.");
        }
        
        var user = await userRepository.GetByIdAsync(query.UserId);
        
        if (user is null)
        {
            return UserErrors.NotFound;
        }
        
        return new TokenResponseDto
        {
            AccessToken = authService.GenerateAccessToken(user), 
            RefreshToken = token.RefreshToken
        };
    }
}