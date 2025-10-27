using CapybaraPetApp.Application.Abstractions.CQRS;
using ErrorOr;

namespace CapybaraPetApp.Application.Interactions.Commands.CreateInteraction;

public record CreateInteractionCommand : ICommand<ErrorOr<Guid>>;