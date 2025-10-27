using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Domain.CapybaraAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Capybaras.Queries;

public record GetCapybaraQuery(Guid CapybaraId) : IQuery<ErrorOr<Capybara>>;