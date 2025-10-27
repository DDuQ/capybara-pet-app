using CapybaraPetApp.Application.Abstractions.CQRS;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Commands.AssignItem;

public record BuyItemCommand(Guid ItemId, Guid UserId) : ICommand<ErrorOr<Success>>;