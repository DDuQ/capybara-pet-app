using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.AchievementAggregare;
using Microsoft.EntityFrameworkCore;

namespace CapybaraPetApp.Infrastructure.Persistence.Repositories;

public class AchievementRepository(CapybaraPetAppDbContext dbContext) : IRepository<Achievement>
{
    private readonly CapybaraPetAppDbContext _dbContext = dbContext;

    public async Task Add(Achievement entity)
    {
        await _dbContext.Achievement.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Achievement?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Achievement.FirstOrDefaultAsync(achievement => achievement.Id == id);
    }

    public async Task Update(Achievement entity)
    {
        _dbContext.Achievement.Update(entity);
        await _dbContext.SaveChangesAsync();
    }
}
