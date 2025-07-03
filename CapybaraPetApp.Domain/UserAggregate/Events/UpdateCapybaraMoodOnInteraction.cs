using CapybaraPetApp.Domain.Common;
using CapybaraPetApp.Domain.Common.EventualConsistency;
using CapybaraPetApp.Domain.Common.JoinTables.Interaction;
using ErrorOr;

namespace CapybaraPetApp.Domain.UserAggregate.Events;

public record UpdateCapybaraMoodOnInteraction(Interaction Interaction) : IDomainEvent
{
    public static readonly Error CapybaraNotFound = EventualConsistencyError.From(
        code: $"{nameof(UpdateCapybaraMoodOnInteraction)}.CapybaraNotFound",
        description: "Capybara not found.");
}