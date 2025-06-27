using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Domain.CapybaraAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Capybaras.Queries;

public record GetCapybaraQuery(Guid CapybaraId) : IQuery<ErrorOr<Capybara>>;
