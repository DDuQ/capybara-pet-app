using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Commands.RegisterUser;

public record RegisterUserCommand(string Username, string Email, string Password, Guid? Id) : ICommand<ErrorOr<User>>;