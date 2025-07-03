namespace CapybaraPetApp.Domain.Common.JoinTables;

public class UserAchievement
{
    public UserAchievement(Guid userId, Guid achievementId)
    {
        UserId = userId;
        AchievementId = achievementId;
        UnlockedAt = DateTimeOffset.UtcNow;
    }

    private UserAchievement() { }

    public Guid UserId { get; set; }
    public Guid AchievementId { get; set; }
    public DateTimeOffset UnlockedAt { get; set; }
}

