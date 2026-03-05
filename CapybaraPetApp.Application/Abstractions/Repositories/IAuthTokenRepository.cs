using CapybaraPetApp.Application.Auth.Utils;

namespace CapybaraPetApp.Application.Abstractions.Repositories;

public interface IAuthTokenRepository
{
    Task<AuthToken?> GetByUserIdAsync(Guid userId);
    void Add(AuthToken authToken);
    void Update(AuthToken authToken);
}