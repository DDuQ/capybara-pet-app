using CapybaraPetApp.Application.Abstractions;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Commands.AssignItem;

public record BuyItemCommand(Guid ItemId, Guid UserId) : ICommand<ErrorOr<Success>>;
