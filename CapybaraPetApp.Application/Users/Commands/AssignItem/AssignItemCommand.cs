using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Users.Commands.AddItem;

public record AssignItemCommand(Guid ItemId, Guid UserId) : IRequest<ErrorOr<Success>>;
