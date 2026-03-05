using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Application.Auth.Utils;
using Microsoft.EntityFrameworkCore;

namespace CapybaraPetApp.Infrastructure.Persistence.Repositories;

public class AuthTokenRepository(CapybaraPetAppDbContext context) : IAuthTokenRepository
{
    public async Task<AuthToken?> GetByUserIdAsync(Guid userId)
    {
        return await context.AuthToken.OrderByDescending(x => x.Expiration).FirstOrDefaultAsync(x => x.UserId == userId);
    }

    public void Add(AuthToken authToken)
    {
        context.AuthToken.Add(authToken);
    }
    
    public void Update(AuthToken authToken)
    {
        context.AuthToken.Update(authToken);
    }
}