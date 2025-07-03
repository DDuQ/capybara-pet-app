using CapybaraPetApp.Domain.Common;
using CapybaraPetApp.Domain.Common.EventualConsistency;
using ErrorOr;

namespace CapybaraPetApp.Domain.UserAggregate.Events;

public record ItemPurchasedEvent(Guid ItemId, Guid UserId) : IDomainEvent
{
    public static readonly Error ItemDoesNotExist = EventualConsistencyError.From(
        code: $"{nameof(ItemPurchasedEvent)}.{nameof(ItemDoesNotExist)}",
        description: "Item does not exist.");
    public static readonly Error UserDoesNotExist = EventualConsistencyError.From(
        code: $"{nameof(ItemPurchasedEvent)}.{nameof(UserDoesNotExist)}",
        description: "User does not exist.");
};