using CapybaraPetApp.Domain.CapybaraAggregate;
using CapybaraPetApp.Domain.Common;

namespace CapybaraPetApp.Domain.UserAggregate.Events;

public record CapybaraAssignedEvent(Capybara Capybara, Guid UserId) : IDomainEvent;
