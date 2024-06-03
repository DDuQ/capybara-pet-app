using ErrorOr;

namespace CapybaraPetApp.Domain.AvatarAggregate;

public class AvatarStats(int happiness, int health, int energy)
{
    private bool IsAvatarExhausted { get; set; } = false;
    public int Happiness { get; set; } = happiness;
    public int Health { get; set; } = health;
    public int Energy { get; set; } = energy;

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

    internal static AvatarStats Empty() => new(happiness: 0, health: 0, energy: 0);
}