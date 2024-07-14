using CapybaraPetApp.Domain.Common;
using CapybaraPetApp.Domain.Common.JoinTables;
using ErrorOr;

namespace CapybaraPetApp.Domain.AchievementAggregate;

//TODO: Add method to retrieve the % of rarity of an achievement.
public class Achievement : AggregateRoot
{
    private List<UserAchievement> _userAchievements { get; set; } = new();
    public AchievementType AchievementType { get; set; }
    public IReadOnlyCollection<UserAchievement> UserAchievements => _userAchievements.ToList();

    public Achievement(AchievementType achievementType) : base(Guid.NewGuid())
    {
        AchievementType = achievementType;
    }

    private Achievement() { }

    public ErrorOr<Success> AssignUserAchievement(UserAchievement userAchievement)
    {
        if (_userAchievements.Any(ua => ua.AchievementId == userAchievement.AchievementId) &&
            _userAchievements.Any(ua => ua.UserId == userAchievement.UserId))
        {
            return Error.Conflict(description: "User achievement already exists.");
        }

        _userAchievements.Add(userAchievement);
        return Result.Success;
    }
}