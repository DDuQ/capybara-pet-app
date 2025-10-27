using CapybaraPetApp.Application.Abstractions.CQRS;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Commands.UseItem;

public record UseItemCommand(Guid UserId, Guid CapybaraId, Guid ItemId, int Amount) : ICommand<ErrorOr<Success>>;