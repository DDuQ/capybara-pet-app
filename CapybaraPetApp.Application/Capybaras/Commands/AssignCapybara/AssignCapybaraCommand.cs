using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Capybaras.Commands.AssignCapybara;

public record AssignCapybaraCommand(Guid UserId, Guid CapybaraId) : IRequest<ErrorOr<Success>>;
