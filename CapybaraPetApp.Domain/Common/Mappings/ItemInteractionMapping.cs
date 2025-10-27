using CapybaraPetApp.Domain.Common.JoinTables.Interaction.Strategies;
using CapybaraPetApp.Domain.ItemAggregate;
using ErrorOr;

namespace CapybaraPetApp.Domain.Common.Mappings;

public static class ItemInteractionMapping
{
    private static readonly Dictionary<ItemType, ErrorOr<IInteractionStrategy>> Mapping = new()
    {
        { ItemType.Fruit, new FeedStrategy() },
        { ItemType.Toy, new PlayStrategy() },
        { ItemType.CleaningTool, new BathStrategy() }
    };

    public static ErrorOr<IInteractionStrategy> TryGetInteractionStrategy(ItemType itemType)
    {
        return Mapping.TryGetValue(itemType, out var interactionStrategy)
            ? interactionStrategy
            : Error.Conflict(description: "Item type not related to any interaction type.");
    }
}