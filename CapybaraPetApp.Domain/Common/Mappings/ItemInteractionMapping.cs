using CapybaraPetApp.Domain.InteractionAggregate;
using CapybaraPetApp.Domain.ItemAggregate;
using ErrorOr;

namespace CapybaraPetApp.Domain.Common.Mappings;

public static class ItemInteractionMapping
{
    private static readonly Dictionary<ItemType, InteractionType> _mapping = new()
    {
        { ItemType.Fruit, InteractionType.Feed },
        { ItemType.Toy, InteractionType.Play },
        { ItemType.CleaningTool, InteractionType.Clean},
    };

    public static ErrorOr<InteractionType> TryGetInteractionType(ItemType itemType)
    {
        if (_mapping.TryGetValue(itemType, out var interactionType))
        {
            return interactionType;
        }

        return Error.Conflict("Item type not related to any interaction type.");
    }
}
