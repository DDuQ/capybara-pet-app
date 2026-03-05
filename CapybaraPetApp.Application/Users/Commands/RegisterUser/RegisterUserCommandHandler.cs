using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;
using Microsoft.AspNetCore.Identity;

namespace CapybaraPetApp.Application.Users.Commands.RegisterUser;

public class RegisterUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<RegisterUserCommand, ErrorOr<User>>
{
    public async Task<ErrorOr<User>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        if (await userRepository.ExistsByEmailAsync(command.Email)) return UserErrors.EmailAlreadyInUse;

        var user = new User(command.Username, command.Email, command.Id);
        var hashedPassword = new PasswordHasher<User>().HashPassword(user, command.Password);
        user.SetHashedPassword(hashedPassword);

        await userRepository.AddAsync(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return user;
    }
}