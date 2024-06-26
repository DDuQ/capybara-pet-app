using CapybaraPetApp.Application.Common;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Capybaras.Commands.AssignCapybara;

public class AssignCapybaraCommandHandler : IRequestHandler<AssignCapybaraCommand, ErrorOr<Success>>
{
    private readonly IUserRepository _userRepository;

    public AssignCapybaraCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<Success>> Handle(AssignCapybaraCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user == null)
        {
            return Error.NotFound("User not found.");
        }

        user.AddCapybara(request.CapybaraId);

        return Result.Success;
    }
}
