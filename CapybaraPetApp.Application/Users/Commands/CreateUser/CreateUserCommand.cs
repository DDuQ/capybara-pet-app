using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Commands.CreateUser;

public record CreateUserCommand(string Username, string Email, string Password, Guid? Id) : ICommand<ErrorOr<User>>;