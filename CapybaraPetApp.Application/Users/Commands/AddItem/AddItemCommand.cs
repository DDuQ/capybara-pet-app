using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Users.Commands.AddItemCommand;

public record AddItemCommand(Guid ItemId, Guid UserId) : IRequest<ErrorOr<Success>>;
