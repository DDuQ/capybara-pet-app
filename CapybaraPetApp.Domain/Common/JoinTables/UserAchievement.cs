using CapybaraPetApp.Domain.AchievementAggregate;
using CapybaraPetApp.Domain.UserAggregate;

namespace CapybaraPetApp.Domain.Common.JoinTables;

public class UserAchievement
{
    public UserAchievement(Guid userId, Guid achievementId)
    {
        UserId = userId;
        AchievementId = achievementId;
        UnlockedAt = DateTimeOffset.UtcNow;
    }

    private UserAchievement()
    {
    } // For EF Core

    public Guid UserId { get; private set; }
    public Guid AchievementId { get; private set; }
    public DateTimeOffset UnlockedAt { get; private set; }
    public User User { get; private set; } = null!;
    public Achievement Achievement { get; private set; } = null!;
}

