namespace CapybaraPetApp.Application.Abstractions.Dtos;

public record CapybaraStatsDto
{
    public CapybaraStatsDto(int happiness, int health, int energy)
    {
        Happiness = happiness;
        Health = health;
        Energy = energy;
    }

    public int Happiness { get; set; }
    public int Health { get; set; }
    public int Energy { get; set; }

    internal static CapybaraStatsDto Empty()
    {
        return new CapybaraStatsDto(0, 0, 0);
    }
}