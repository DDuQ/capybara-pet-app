using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.AchievementAggregate;
using Microsoft.EntityFrameworkCore;

namespace CapybaraPetApp.Infrastructure.Persistence.Repositories;

public class AchievementRepository : Repository<Achievement>, IAchievementRepository
{
    private readonly DbSet<Achievement> _achievement;

    public AchievementRepository(CapybaraPetAppDbContext dbContext) : base(dbContext)
    {
        _achievement = dbContext.Achievement;
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await _achievement.AnyAsync(ach => string.Equals(ach.AchievementType.Name, name, StringComparison.OrdinalIgnoreCase));
    }
}