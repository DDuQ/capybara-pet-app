using CapybaraPetApp.Domain.Common;
using CapybaraPetApp.Domain.Common.EventualConsistency;
using ErrorOr;

namespace CapybaraPetApp.Domain.ItemAggregate.Events;

public record ApplyItemEffectToCapybaraEvent(Guid UserId, Guid ItemId, Guid CapybaraId, int Quantity) : IDomainEvent
{
    public static readonly Error UserDoesNotExist = EventualConsistencyError.From(
        $"{nameof(ApplyItemEffectToCapybaraEvent)}.{nameof(UserDoesNotExist)}",
        "User does not exists.");

    public static readonly Error ItemDoesNotExist = EventualConsistencyError.From(
        $"{nameof(ApplyItemEffectToCapybaraEvent)}.{nameof(ItemDoesNotExist)}",
        "Item does not exists.");

    public static readonly Error InsufficientItem = EventualConsistencyError.From(
        $"{nameof(ApplyItemEffectToCapybaraEvent)}.{nameof(InsufficientItem)}",
        "Insufficient item quantity.");

    public static readonly Error InvalidInteractionType = EventualConsistencyError.From(
        $"{nameof(ApplyItemEffectToCapybaraEvent)}.{nameof(InvalidInteractionType)}",
        "Invalid interaction type.");

    public static readonly Error InvalidInteractionDetail = EventualConsistencyError.From(
        $"{nameof(ApplyItemEffectToCapybaraEvent)}.{nameof(InvalidInteractionDetail)}",
        "Invalid interaction detail.");

    public static readonly Error CapybaraDoesNotExist = EventualConsistencyError.From(
        $"{nameof(ApplyItemEffectToCapybaraEvent)}.{nameof(CapybaraDoesNotExist)}",
        "Capybara does not exists.");

    public static readonly Error ItemHasNotBeenAssigned = EventualConsistencyError.From(
        $"{nameof(ApplyItemEffectToCapybaraEvent)}.{nameof(ItemHasNotBeenAssigned)}",
        "Item has not been assigned to the user.");
}