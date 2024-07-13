using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Users.Commands.CreateUser;

public record CreateUserCommand(string Username, string Email, Guid? Id) : IRequest<ErrorOr<User>>;
