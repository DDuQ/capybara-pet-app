using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Domain.CapybaraAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Capybaras.Commands.CreateCapybara;

public record CreateCapybaraCommand(string Name) : ICommand<ErrorOr<Capybara>>;