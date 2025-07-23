using CapybaraPetApp.Domain.AchievementAggregate;

namespace CapybaraPetApp.Application.Common;

public interface IAchievementRepository
{
    Task AddAsync(Achievement achievement);

    Task<Achievement?> GetByIdAsync(Guid id);

    Task UpdateAsync(Achievement achievement);

    Task<bool> ExistsByNameAsync(string title);
}