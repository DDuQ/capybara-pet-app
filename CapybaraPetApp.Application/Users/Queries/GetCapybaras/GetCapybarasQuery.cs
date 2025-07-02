using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Domain.CapybaraAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Queries.GetCapybaras;

public record GetCapybarasQuery(Guid UserId) : IQuery<ErrorOr<List<Capybara>>>;