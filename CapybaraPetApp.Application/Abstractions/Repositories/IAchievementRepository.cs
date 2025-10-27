using CapybaraPetApp.Domain.AchievementAggregate;

namespace CapybaraPetApp.Application.Abstractions.Repositories;

public interface IAchievementRepository : IRepository<Achievement>
{
    Task<bool> ExistsByNameAsync(string title);
}