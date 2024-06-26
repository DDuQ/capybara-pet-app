using CapybaraPetApp.Domain.Common;

namespace CapybaraPetApp.Domain.AchievementAggregare;

public class Achievement : AggregateRoot
{
    public Achievement(
        Guid id,
        AchievementType achievementType,
        DateTime dateAchieved,
        Guid userId) : base(id)
    {
        AchievementType = achievementType;
        DateAchieved = dateAchieved;
        UserId = userId;
    }

    private Achievement()
    {
    }

    public AchievementType AchievementType { get; set; }
    public DateTime DateAchieved { get; set; }
    public Guid UserId { get; set; }
}