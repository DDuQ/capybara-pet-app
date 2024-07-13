using CapybaraPetApp.Domain.CapybaraAggregate;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Capybaras.Queries;

public record GetCapybaraQuery(Guid CapybaraId) : IRequest<ErrorOr<Capybara>>;
