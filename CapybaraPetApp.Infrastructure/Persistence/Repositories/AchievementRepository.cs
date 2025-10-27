using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Domain.AchievementAggregate;
using Microsoft.EntityFrameworkCore;

namespace CapybaraPetApp.Infrastructure.Persistence.Repositories;

public class AchievementRepository(CapybaraPetAppDbContext dbContext)
    : Repository<Achievement>(dbContext), IAchievementRepository
{
    private readonly DbSet<Achievement> _achievements = dbContext.Achievement;

    public async Task<bool> ExistsByNameAsync(string title)
    {
        return await _achievements.AnyAsync(ach => string.Equals(ach.Title, title, StringComparison.OrdinalIgnoreCase));
    }
}