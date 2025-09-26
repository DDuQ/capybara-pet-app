using ErrorOr;

namespace CapybaraPetApp.Domain.CapybaraAggregate;

public record CapybaraStats
{
    public int Happiness { get; private set; }
    public int Health { get; private set; }
    public int Energy { get; private set; }

    public CapybaraStats(int happiness, int health, int energy)
    {
        Happiness = happiness;
        Health = health;
        Energy = energy;
    }
    
    internal static CapybaraStats InitialState() => new(happiness: 100, health: 100, energy: 100);

    public void Update(CapybaraStats capybaraStats)
    {
        Health = capybaraStats.Health;
        Happiness = capybaraStats.Happiness;
        Energy = capybaraStats.Energy;
    }
}