using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;
using Microsoft.AspNetCore.Identity;

namespace CapybaraPetApp.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<User>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        if (await _userRepository.ExistsByEmail(command.Email)) return UserErrors.EmailAlreadyInUse;

        //TODO: Make JWT Auth implementation.
        var user = new User(command.Username, command.Email, command.Id);
        user.AddHashedPassword(new PasswordHasher<User>().HashPassword(user, command.Password));

        await _userRepository.AddAsync(user);

        return user;
    }
}