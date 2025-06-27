using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Queries.GetUser;

public record GetUserQuery(Guid Id) : IQuery<ErrorOr<User>>;
