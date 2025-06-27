using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Commands.CreateUser;

public record CreateUserCommand(string Username, string Email, Guid? Id) : ICommand<ErrorOr<User>>;
