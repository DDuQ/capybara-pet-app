using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;
using Microsoft.AspNetCore.Identity;

namespace CapybaraPetApp.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateUserCommand, ErrorOr<User>>
{
    public async Task<ErrorOr<User>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        if (await userRepository.ExistsByEmail(command.Email)) return UserErrors.EmailAlreadyInUse;

        //TODO: Make JWT Auth implementation instead of leveraging Identity provider (Keycloak). [Debatable]
        var user = new User(command.Username, command.Email, command.Id);
        user.AddHashedPassword(new PasswordHasher<User>().HashPassword(user, command.Password));

        await userRepository.AddAsync(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return user;
    }
}