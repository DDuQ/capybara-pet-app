using CapybaraPetApp.Domain.Common;

namespace CapybaraPetApp.Domain.AchievementAggregate;

//TODO: Add method to retrieve the % of rarity of an achievement.
public class Achievement : AggregateRoot
{
    public AchievementType AchievementType { get; set; }

    public Achievement(AchievementType achievementType) : base(Guid.NewGuid())
    {
        AchievementType = achievementType;
    }

    private Achievement() { }
}