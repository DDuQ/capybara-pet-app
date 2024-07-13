using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace CapybaraPetApp.Infrastructure.Persistence.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly CapybaraPetAppDbContext _dbContext;

    public UserRepository(CapybaraPetAppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> ExistsByEmail(string email)
    {
        return await _dbContext.User.AnyAsync(x => x.Email == email);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _dbContext.User
                           .Include(u => u.UserAchievements)
                           .Include(u => u.Capybaras)
                           .Include(u => u.Interactions)
                           .Include(u => u.Items)
                           .ToListAsync();
    }
}