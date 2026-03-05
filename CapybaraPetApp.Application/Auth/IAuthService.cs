using CapybaraPetApp.Application.Auth.Utils;
using CapybaraPetApp.Domain.UserAggregate;

namespace CapybaraPetApp.Application.Auth;

public interface IAuthService
{
    string GenerateAccessToken(User user);
    Task<AuthToken> GenerateAndSaveRefreshToken(Guid userId);
    Task<AuthToken?> IssueNewRefreshToken(Guid userId, string refreshToken);
}