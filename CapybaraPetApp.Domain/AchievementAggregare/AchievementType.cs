using CapybaraPetApp.Domain.Common;

namespace CapybaraPetApp.Domain.AchievementAggregare;

public class AchievementType(string name, string description) : ValueObject
{
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Description;
    }
}