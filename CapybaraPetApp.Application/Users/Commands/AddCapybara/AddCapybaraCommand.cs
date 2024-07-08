using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Users.Commands.AssignCapybara;

public record AddCapybaraCommand(Guid UserId, Guid CapybaraId) : IRequest<ErrorOr<Success>>;
