using CapybaraPetApp.Domain.Common;
using CapybaraPetApp.Domain.Common.EventualConsistency;
using ErrorOr;

namespace CapybaraPetApp.Domain.ItemAggregate.Events;

public record ApplyItemEffectToCapybaraEvent(Guid UserId, Guid ItemId, Guid CapybaraId, int Quantity) : IDomainEvent
{
    public static readonly Error UserDoesNotExist = EventualConsistencyError.From(
        code: $"{nameof(ApplyItemEffectToCapybaraEvent)}.{nameof(UserDoesNotExist)}",
        description: "User does not exists.");

    public static readonly Error ItemDoesNotExist = EventualConsistencyError.From(
        code: $"{nameof(ApplyItemEffectToCapybaraEvent)}.{nameof(ItemDoesNotExist)}",
        description: "Item does not exists.");

    public static readonly Error InsufficientItem = EventualConsistencyError.From(
        code: $"{nameof(ApplyItemEffectToCapybaraEvent)}.{nameof(InsufficientItem)}",
    description: "Insufficient item quantity.");

    public static readonly Error InvalidInteractionType = EventualConsistencyError.From(
        code: $"{nameof(ApplyItemEffectToCapybaraEvent)}.{nameof(InvalidInteractionType)}",
        description: "Invalid interaction type.");

    public static readonly Error InvalidInteractionDetail = EventualConsistencyError.From(
        code: $"{nameof(ApplyItemEffectToCapybaraEvent)}.{nameof(InvalidInteractionDetail)}",
        description: "Invalid interaction detail.");
}