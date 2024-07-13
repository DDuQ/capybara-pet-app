using CapybaraPetApp.Domain.Common.JoinTables.Interaction;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Users.Commands.AddInteraction;

public record AddInteractionCommand(Guid UserId, Guid CapybaraId, InteractionDetail InteractionDetail) : IRequest<ErrorOr<Success>>;
