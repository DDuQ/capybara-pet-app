using CapybaraPetApp.Domain.AchievementAggregare;

namespace CapybaraPetApp.Application.Common;

public interface IAchievementRepository
{
    Task AddAsync(Achievement achievement);

    Task<Achievement?> GetByIdAsync(Guid id);

    Task UpdateAsync(Achievement achievement);
}