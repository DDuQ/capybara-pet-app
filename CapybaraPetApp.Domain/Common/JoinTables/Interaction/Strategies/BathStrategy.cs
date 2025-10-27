using CapybaraPetApp.Domain.CapybaraAggregate;
using ErrorOr;

namespace CapybaraPetApp.Domain.Common.JoinTables.Interaction.Strategies;

public class BathStrategy : IInteractionStrategy
{
    public ErrorOr<Success> Validate(int bathTimeHours)
    {
        return bathTimeHours switch
        {
            <= 0 => InteractionErrors.QuantityCannotBeLessThanOne,
            > 1 => InteractionErrors.CannotTakeMoreThanAnHour,
            _ => Result.Success
        };
    }

    public ErrorOr<Success> Apply(Capybara capybara, int quantity)
    {
        if (quantity <= 0)
            return CapybaraErrors.InvalidStatChange;

        var health = IInteractionStrategy.ClampStatValue(capybara.Stats.Health + quantity + 2);
        var happiness = IInteractionStrategy.ClampStatValue(capybara.Stats.Happiness + quantity * 2);

        var capybaraStats = new CapybaraStats(happiness, health, capybara.Stats.Energy);

        capybara.UpdateStats(capybaraStats);

        return Result.Success;
    }
}