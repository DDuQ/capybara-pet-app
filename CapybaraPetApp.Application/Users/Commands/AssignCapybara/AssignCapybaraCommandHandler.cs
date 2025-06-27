using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Common;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Commands.AssignCapybara;

public class AssignCapybaraCommandHandler : ICommandHandler<AssignCapybaraCommand, ErrorOr<Success>>
{
    private readonly IUserRepository _userRepository;
    private readonly ICapybaraRepository _capybaraRepository;

    public AssignCapybaraCommandHandler(IUserRepository userRepository, ICapybaraRepository capybaraRepository)
    {
        _userRepository = userRepository;
        _capybaraRepository = capybaraRepository;
    }

    public async Task<ErrorOr<Success>> Handle(AssignCapybaraCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(command.UserId);

        if (user == null)
        {
            return Error.NotFound(description: "User not found.");
        }

        var capybara = await _capybaraRepository.GetByIdAsync(command.CapybaraId);

        if (capybara == null)
        {
            return Error.NotFound(description: "Capybara not found.");
        }

        user.AssignCapybara(capybara);

        await _userRepository.UpdateAsync(user);

        return Result.Success;
    }
}
