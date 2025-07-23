using ErrorOr;

namespace CapybaraPetApp.Domain.CapybaraAggregate;

public record CapybaraStats
{
    private const int MaxStatValue = 100;
    private const int MinStatValue = 0;
    
    private bool IsExhausted => Energy <= 0;
    
    public int Happiness { get; private set; }
    public int Health { get; private set; }
    public int Energy { get; private set; }

    private CapybaraStats(int happiness, int health, int energy)
    {
        Happiness = ClampStatValue(happiness);
        Health = ClampStatValue(health);
        Energy = ClampStatValue(energy);
    }

    internal ErrorOr<Success> Feed(int amount)
    {
        if (amount <= 0)
            return CapybaraErrors.InvalidStatChange;
            
        Health = ClampStatValue(Health + amount);
        Energy = ClampStatValue(Energy + (amount / 2));
        return Result.Success;
    }

    internal ErrorOr<Success> Play(int amount)
    {
        if (amount <= 0)
            return CapybaraErrors.InvalidStatChange;
            
        if (IsExhausted)
            return CapybaraErrors.InsufficientEnergy(Energy);

        Happiness = ClampStatValue(Happiness + amount);
        Energy = ClampStatValue(Energy - (amount / 2));
        return Result.Success;
    }

    internal ErrorOr<Success> Clean(int amount)
    {
        if (amount <= 0)
            return CapybaraErrors.InvalidStatChange;
            
        Health = ClampStatValue(Health + amount + 2);
        Happiness = ClampStatValue(Happiness + (amount * 2));
        return Result.Success;
    }
    
    private static int ClampStatValue(int value) => 
        Math.Clamp(value, MinStatValue, MaxStatValue);

    internal static CapybaraStats Empty() => new(happiness: 0, health: 0, energy: 0);
    
    public static ErrorOr<CapybaraStats> Create(int happiness, int health, int energy)
    {
        if (happiness < MinStatValue || happiness > MaxStatValue)
            return CapybaraErrors.InvalidHappiness;
            
        if (health < MinStatValue || health > MaxStatValue)
            return CapybaraErrors.InvalidHealth;
            
        if (energy < MinStatValue || energy > MaxStatValue)
            return CapybaraErrors.InvalidEnergy;
            
        return new CapybaraStats(happiness, health, energy);
    }
}