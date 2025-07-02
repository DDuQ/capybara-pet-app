using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, ErrorOr<Guid>>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        if (await _userRepository.ExistsByEmail(command.Email))
        {
            return Error.Conflict(description: "There is an user that already uses that email."); //TODO: Add error code to Domain (UserErrors).
        }

        var user = new User(command.Username, command.Email, command.Id);

        await _userRepository.AddAsync(user);

        return user.Id;
    }
}
