using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Users.Commands.AddCapybara;

public record AssignCapybaraCommand(Guid UserId, Guid CapybaraId) : IRequest<ErrorOr<Success>>;
