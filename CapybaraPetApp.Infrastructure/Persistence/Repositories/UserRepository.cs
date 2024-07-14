using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace CapybaraPetApp.Infrastructure.Persistence.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly DbSet<User> _user;

    public UserRepository(CapybaraPetAppDbContext dbContext) : base(dbContext)
    {
        _user = dbContext.User;
    }

    public async Task<bool> ExistsByEmail(string email)
    {
        return await _user.AnyAsync(x => x.Email == email);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _user
            .Include(u => u.UserAchievements)
            .Include(u => u.Capybaras)
            .Include(u => u.Interactions)
            .Include(u => u.UserItems)
            .ToListAsync();
    }
}