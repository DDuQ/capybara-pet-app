using CapybaraPetApp.Domain.CapybaraAggregate;
using ErrorOr;

namespace CapybaraPetApp.Domain.Common.JoinTables.Interaction.Strategies;

public interface IInteractionStrategy
{
    private const int MaxStatValue = 100;
    private const int MinStatValue = 0;
    
    public static bool IsExhausted(int energy) => energy <= 0;
    public static int ClampStatValue(int value) => 
        Math.Clamp(value, MinStatValue, MaxStatValue);
    public ErrorOr<Success> Validate(int quantity);

    public ErrorOr<Success> Apply(Capybara capybara, int quantity);
}