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
        // Calculate the percentage based on the rarity
        // Lower enum values represent rarer achievements
        var totalRarities = Enum.GetValues(typeof(Rarity)).Length;
        var rarityValue = (int)Rarity;
        
        // Convert to percentage (1.0 = 100% for most common, approaching 0% for rarest)
        var percentage = 1.0 - (double)rarityValue / (totalRarities - 1);
        
        // Convert to actual percentage (0-100%)
        return percentage * 100;
    }

    private Achievement() { }
}

public enum Rarity
{
    Common,     // Most common achievements
    Uncommon,   // Somewhat rare
    Rare,       // Hard to get
    Epic,       // Very rare
    Legendary   // Extremely rare
}