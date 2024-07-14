using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Users.Commands.UseItem;

public record UseItemCommand(Guid UserId, Guid capybaraId, Guid ItemId, int Amount) : IRequest<ErrorOr<Success>>;
