using CapybaraPetApp.Domain.CapybaraAggregate;
using ErrorOr;

namespace CapybaraPetApp.Domain.Common.JoinTables.Interaction.Strategies;

public class FeedStrategy : IInteractionStrategy
{
    public ErrorOr<Success> Validate(int fruitQuantity)
    {
        return fruitQuantity switch
        {
            <= 0 => InteractionErrors.QuantityCannotBeLessThanOne,
            > 100 => InteractionErrors.TooMuchFruit,
            _ => Result.Success
        };
    }

    public ErrorOr<Success> Apply(Capybara capybara, int quantity)
    {
            if (quantity <= 0)
                return CapybaraErrors.InvalidStatChange;
            
            var health = IInteractionStrategy.ClampStatValue(capybara.Stats.Health + quantity);
            var energy = IInteractionStrategy.ClampStatValue(capybara.Stats.Energy + (quantity / 2));
            
            var capybaraStats = new CapybaraStats(capybara.Stats.Happiness, health, energy);
        
            capybara.UpdateStats(capybaraStats);
            
            return Result.Success;
    }
}