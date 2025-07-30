using CapybaraPetApp.Domain.Common;
using ErrorOr;

namespace CapybaraPetApp.Domain.AchievementAggregate;

public class Achievement : AggregateRoot
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public int Points { get; private set; }
    public Rarity Rarity { get; private set; }

    private Achievement(
        string title,
        string description,
        int points,
        Rarity rarity,
        Guid? id = null)
        : base(id ?? Guid.NewGuid())
    {
        Title = title;
        Description = description;
        Points = points;
        Rarity = rarity;
    }

    public static ErrorOr<Achievement> Create(
        string title,
        string description,
        int points,
        Rarity rarity,
        Guid? id = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            return AchievementErrors.InvalidTitle;
            
        if (string.IsNullOrWhiteSpace(description))
            return AchievementErrors.InvalidDescription;
            
        if (points <= 0)
            return AchievementErrors.InvalidPoints;
            
        if (!Enum.IsDefined(typeof(Rarity), rarity))
            return AchievementErrors.InvalidRarity;
            
        return new Achievement(title, description, points, rarity, id);
    }
    
    public double GetRarityPercentage()
    {
        //TODO: Check this logic in the future. Not urgent.
        var totalRarities = Enum.GetValues(typeof(Rarity)).Length;
        var rarityValue = (int)Rarity;
        
        var percentage = 1.0 - (double)rarityValue / (totalRarities - 1);
        
        return percentage * 100;
    }

    private Achievement() { }
}

public enum Rarity
{
    Common,     
    Uncommon,
    Rare,    
    Epic,    
    Legendary
}