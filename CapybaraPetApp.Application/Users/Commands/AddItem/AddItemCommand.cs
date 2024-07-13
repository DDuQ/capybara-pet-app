using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Users.Commands.AddItem;

public record AddItemCommand(Guid ItemId, Guid UserId) : IRequest<ErrorOr<Success>>;
