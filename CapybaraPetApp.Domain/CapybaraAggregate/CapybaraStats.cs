namespace CapybaraPetApp.Domain.CapybaraAggregate;

public record CapybaraStats
{
    public CapybaraStats(int happiness, int health, int energy)
    {
        Happiness = happiness;
        Health = health;
        Energy = energy;
    }

    public int Happiness { get; private set; }
    public int Health { get; private set; }
    public int Energy { get; private set; }

    internal static CapybaraStats InitialState()
    {
        return new CapybaraStats(100, 100, 100);
    }

    public void Update(CapybaraStats capybaraStats)
    {
        Health = capybaraStats.Health;
        Happiness = capybaraStats.Happiness;
        Energy = capybaraStats.Energy;
    }
}