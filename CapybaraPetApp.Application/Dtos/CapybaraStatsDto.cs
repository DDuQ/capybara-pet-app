namespace CapybaraPetApp.Application.Dtos;

public record CapybaraStatsDto
{
    public int Happiness { get; set; }
    public int Health { get; set; }
    public int Energy { get; set; }

    public CapybaraStatsDto(int happiness, int health, int energy)
    {
        Happiness = happiness;
        Health = health;
        Energy = energy;
    }

    internal static CapybaraStatsDto Empty() => new(happiness: 0, health: 0, energy: 0);
}