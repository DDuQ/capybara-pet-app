using CapybaraPetApp.Domain.Common.JoinTables.Interaction;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Interactions.Commands.CreateInteraction;

public record CreateInteractionCommand() : IRequest<ErrorOr<Interaction>>;