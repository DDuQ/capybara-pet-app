using CapybaraPetApp.Domain.CapybaraAggregate;
using ErrorOr;

namespace CapybaraPetApp.Domain.Common.JoinTables.Interaction.Strategies;

public class PlayStrategy : IInteractionStrategy
{
    public ErrorOr<Success> Validate(int playTimeHours)
    {
        return playTimeHours switch
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

        if (IInteractionStrategy.IsExhausted(capybara.Stats.Energy))
            return CapybaraErrors.InsufficientEnergy(capybara.Stats.Energy);

        var happiness = IInteractionStrategy.ClampStatValue(capybara.Stats.Happiness + quantity);
        var energy = IInteractionStrategy.ClampStatValue(capybara.Stats.Energy - quantity / 2);

        var capybaraStats = new CapybaraStats(happiness, capybara.Stats.Health, energy);

        capybara.UpdateStats(capybaraStats);
        return Result.Success;
    }
}