using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Domain.CapybaraAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Capybaras.Commands.CreateCapybara;

public record CreateCapybaraCommand(string Name, Guid? Id, CapybaraStats? Stats) : ICommand<ErrorOr<Capybara>>;
