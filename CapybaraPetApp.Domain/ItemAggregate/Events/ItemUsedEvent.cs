using CapybaraPetApp.Domain.Common;
using CapybaraPetApp.Domain.Common.EventualConsistency;
using CapybaraPetApp.Domain.Common.JoinTables;
using CapybaraPetApp.Domain.Common.JoinTables.Interaction;
using ErrorOr;

namespace CapybaraPetApp.Domain.ItemAggregate.Events;

public record ItemUsedEvent(Guid UserId, Guid CapybaraId, InteractionDetail InteractionDetail, UserItem UserItem) : IDomainEvent
{
    public static readonly Error UserDoesNotExist = EventualConsistencyError.From(
        code: $"{nameof(ItemUsedEvent)}.{nameof(UserDoesNotExist)}",
        description: "User does not exists.");

    public static readonly Error CapybaraDoesNotExist = EventualConsistencyError.From(
        code: $"{nameof(ItemUsedEvent)}.{nameof(CapybaraDoesNotExist)}",
        description: "Capybara does not exists.");

    public static readonly Error CapybaraIsNotOwnedByThisUser = EventualConsistencyError.From(
        code: $"{nameof(ItemUsedEvent)}.{nameof(CapybaraIsNotOwnedByThisUser)}",
        description: "Capybara is not owned by this user.");

    public static readonly Error InvalidInteractionDetail = EventualConsistencyError.From(
        code: $"{nameof(ItemUsedEvent)}.{nameof(InvalidInteractionDetail)}",
        description: "Invalid interaction detail.");
}