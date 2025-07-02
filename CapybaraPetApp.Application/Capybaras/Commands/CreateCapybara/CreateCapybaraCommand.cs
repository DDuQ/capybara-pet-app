using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Domain.CapybaraAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Capybaras.Commands.CreateCapybara;

public record CreateCapybaraCommand(string Name, CapybaraStats? Stats) : ICommand<ErrorOr<Guid>>;
