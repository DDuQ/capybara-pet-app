using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Domain.CapybaraAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Queries.GetCapybaras;

public record GetCapybarasQuery(Guid UserId) : IQuery<ErrorOr<List<Capybara>>>;