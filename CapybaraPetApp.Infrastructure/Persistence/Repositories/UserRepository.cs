using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace CapybaraPetApp.Infrastructure.Persistence.Repositories;

//TODO: Use Dapper on all query repositories for performance and use EF Core only for commands. [Debatable]
public class UserRepository(CapybaraPetAppDbContext dbContext) : Repository<User>(dbContext), IUserRepository
{
    private readonly DbSet<User> _users = dbContext.User;

    public async Task<bool> ExistsByEmail(string email)
    {
        return await _users.AnyAsync(x => x.Email == email);
    }

    public override async Task<User?> GetByIdAsync(Guid id)
    {
        return await _users
            .Include(u => u.UserAchievements)
            .ThenInclude(ua => ua.Achievement)
            .Include(u => u.Interactions)
            .Include(u => u.UserItems)
            .ThenInclude(ui => ui.Item)
            .Include(u => u.UserCapybaras)
            .ThenInclude(uc => uc.Capybara)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _users
            .Include(u => u.UserAchievements)
            .Include(u => u.Interactions)
            .Include(u => u.UserItems)
            .ToListAsync();
    }
}