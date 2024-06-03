namespace CapybaraPetApp.Domain.AchievementAggregare;

public class Achievement(Guid id, AchievementType achievementType, DateTime dateAchieved, Guid userId)
{
    public Guid Id { get; set; } = id;
    public AchievementType AchievementType { get; set; } = achievementType;
    public DateTime DateAchieved { get; set; } = dateAchieved;
    public Guid UserId { get; set; } = userId;
}