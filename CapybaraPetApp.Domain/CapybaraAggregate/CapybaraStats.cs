using ErrorOr;

namespace CapybaraPetApp.Domain.CapybaraAggregate;

public record CapybaraStats
{
    private bool IsAvatarExhausted { get; set; } = false;
    public int Happiness { get; set; }
    public int Health { get; set; }
    public int Energy { get; set; }

    public CapybaraStats(int happiness, int health, int energy)
    {
        Happiness = happiness;
        Health = health;
        Energy = energy;
    }

    private CapybaraStats() { }

    internal ErrorOr<Success> Feed(int amount)
    {
        Health += amount;
        Energy += amount / 2;
        return Result.Success;
    }

    internal ErrorOr<Success> Play(int amount)
    {
        if (IsAvatarExhausted)
        {
            return Error.Conflict($"Capy does not have enough energy (currently: {Energy}) to play right now. :c");
        }

        Happiness += amount;
        Energy -= amount / 2;
        IsAvatarExhausted = Energy <= 0;
        return Result.Success;
    }

    internal ErrorOr<Success> Clean(int amount)
    {
        Health += amount + 2;
        Happiness += amount * 2;
        return Result.Success;
    }

    internal static CapybaraStats Empty() => new(happiness: 0, health: 0, energy: 0);
}