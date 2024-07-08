using CapybaraPetApp.Application.Common;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Users.Commands.AssignCapybara;

public class AddCapybaraCommandHandler : IRequestHandler<AddCapybaraCommand, ErrorOr<Success>>
{
    private readonly IUserRepository _userRepository;
    private readonly ICapybaraRepository _capybaraRepository;

    public AddCapybaraCommandHandler(IUserRepository userRepository, ICapybaraRepository capybaraRepository)
    {
        _userRepository = userRepository;
        _capybaraRepository = capybaraRepository;
    }

    public async Task<ErrorOr<Success>> Handle(AddCapybaraCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user == null)
        {
            return Error.NotFound("User not found.");
        }

        var capybara = await _capybaraRepository.GetByIdAsync(request.CapybaraId);

        if (capybara == null)
        {
            return Error.NotFound("Capybara not found.");
        }

        user.AddCapybara(capybara);

        return Result.Success;
    }
}
