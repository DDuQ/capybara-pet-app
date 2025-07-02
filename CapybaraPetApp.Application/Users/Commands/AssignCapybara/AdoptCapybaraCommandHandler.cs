using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Common;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Commands.AssignCapybara;

public class AdoptCapybaraCommandHandler : ICommandHandler<AdoptCapybaraCommand, ErrorOr<Success>>
{
    private readonly IUserRepository _userRepository;
    private readonly ICapybaraRepository _capybaraRepository;

    public AdoptCapybaraCommandHandler(IUserRepository userRepository, ICapybaraRepository capybaraRepository)
    {
        _userRepository = userRepository;
        _capybaraRepository = capybaraRepository;
    }

    public async Task<ErrorOr<Success>> Handle(AdoptCapybaraCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(command.UserId);

        if (user == null)
        {
            return Error.NotFound(description: "User not found."); //TODO: Add error code to Domain (UserErrors).
        }

        var capybara = await _capybaraRepository.GetByIdAsync(command.CapybaraId);

        if (capybara == null)
        {
            return Error.NotFound(description: "Capybara not found."); //TODO: Add error code to Domain (CapybaraErrors).
        }

        user.AssignCapybara(capybara);

        await _userRepository.UpdateAsync(user);

        return Result.Success;
    }
}
