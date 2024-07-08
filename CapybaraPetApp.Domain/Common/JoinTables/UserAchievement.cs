using CapybaraPetApp.Domain.AchievementAggregate;
using CapybaraPetApp.Domain.UserAggregate;

namespace CapybaraPetApp.Domain.Common.JoinTables;

public class UserAchievement
{
    public UserAchievement(Guid userId, Guid achievementId)
    {
        UserId = userId;
        AchievementId = achievementId;
    }

    private UserAchievement() { }

    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid AchievementId { get; set; }
    public Achievement Achievement { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

