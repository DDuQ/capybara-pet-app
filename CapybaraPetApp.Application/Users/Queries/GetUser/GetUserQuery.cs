using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Users.Queries.GetUser;

public record GetUserQuery(Guid Id) : IRequest<ErrorOr<User>>;
