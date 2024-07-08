namespace CapybaraPetApp.Domain.AchievementAggregate;

public record AchievementType
{
    public AchievementType(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public string Name { get; set; }
    public string Description { get; set; }
}