using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Domain.Common.JoinTables.Interaction;
using ErrorOr;

namespace CapybaraPetApp.Application.Interactions.Commands.CreateInteraction;

public record CreateInteractionCommand() : ICommand<ErrorOr<Interaction>>;