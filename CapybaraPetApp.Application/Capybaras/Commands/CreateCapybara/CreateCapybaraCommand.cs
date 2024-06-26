using CapybaraPetApp.Domain.CapybaraAggregate;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Capybaras.Commands.CreateCapybara;

public record CreateCapybaraCommand(
    string Name,
    Guid UserId,
    CapybaraStats? Stats) : IRequest<ErrorOr<Capybara>>;
