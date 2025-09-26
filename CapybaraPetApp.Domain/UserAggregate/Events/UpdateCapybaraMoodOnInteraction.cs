using CapybaraPetApp.Domain.Common;
using CapybaraPetApp.Domain.Common.EventualConsistency;
using CapybaraPetApp.Domain.Common.JoinTables.Interaction;
using CapybaraPetApp.Domain.Common.JoinTables.Interaction.Strategies;
using ErrorOr;

namespace CapybaraPetApp.Domain.UserAggregate.Events;

public record UpdateCapybaraMoodOnInteraction(Guid CapybaraId, IInteractionStrategy InteractionStrategy, int Quantity) : IDomainEvent
{
    public static readonly Error CapybaraNotFound = EventualConsistencyError.From(
        code: $"{nameof(UpdateCapybaraMoodOnInteraction)}.CapybaraNotFound",
        description: "Capybara not found.");
}