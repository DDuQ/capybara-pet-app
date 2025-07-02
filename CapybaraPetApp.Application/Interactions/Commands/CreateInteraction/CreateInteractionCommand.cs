using CapybaraPetApp.Application.Abstractions;
using ErrorOr;

namespace CapybaraPetApp.Application.Interactions.Commands.CreateInteraction;

public record CreateInteractionCommand() : ICommand<ErrorOr<Guid>>;