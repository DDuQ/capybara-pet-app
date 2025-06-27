using CapybaraPetApp.Application.Abstractions;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Commands.AssignCapybara;

public record AssignCapybaraCommand(Guid UserId, Guid CapybaraId) : ICommand<ErrorOr<Success>>;
