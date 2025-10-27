using CapybaraPetApp.Application.Abstractions.CQRS;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Commands.AdoptCapybara;

public record AdoptCapybaraCommand(Guid UserId, Guid CapybaraId) : ICommand<ErrorOr<Success>>;