using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ErrorOr<User>>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.ExistsByEmail(request.Email))
        {
            return Error.Conflict(description:"There is an user that already uses that email.");
        }

        var user = new User(request.Username, request.Email, request.Id);

        await _userRepository.AddAsync(user);

        return user;
    }
}
