using CapybaraPetApp.Domain.Common;

namespace CapybaraPetApp.Domain.UserAggregate.Events;

public record CapybaraAdoptedEvent(Guid CapybaraId, Guid UserId) : IDomainEvent;
