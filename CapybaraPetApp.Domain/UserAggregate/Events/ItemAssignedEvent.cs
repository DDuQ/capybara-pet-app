using CapybaraPetApp.Domain.Common;
using CapybaraPetApp.Domain.ItemAggregate;

namespace CapybaraPetApp.Domain.UserAggregate.Events;

public record ItemAssignedEvent(Item Item, User User) : IDomainEvent;