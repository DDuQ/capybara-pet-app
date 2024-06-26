using CapybaraPetApp.Domain.CapybaraAggregate;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Users.Queries.GetCapybara;

public record GetCapybarasQuery(
    Guid UserId) : IRequest<ErrorOr<List<Capybara>>>;