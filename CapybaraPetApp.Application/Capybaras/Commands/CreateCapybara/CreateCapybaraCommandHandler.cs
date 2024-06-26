using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.CapybaraAggregate;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Capybaras.Commands.CreateCapybara;

public class CreateCapybaraCommandHandler : IRequestHandler<CreateCapybaraCommand, ErrorOr<Capybara>>
{
    private readonly IUserRepository _userRepository;
    private readonly ICapybaraRepository _capybaraRepository;

    public CreateCapybaraCommandHandler(IUserRepository userRepository, ICapybaraRepository capybaraRepository)
    {
        _userRepository = userRepository;
        _capybaraRepository = capybaraRepository;
    }

    public async Task<ErrorOr<Capybara>> Handle(CreateCapybaraCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            return Error.NotFound("User not found.");
        }

        var capybara = new Capybara(
            request.Name,
            request.UserId,
            request.Stats);

        await _capybaraRepository.AddAsync(capybara);

        user.AddCapybara(capybara.Id);

        return capybara;
    }
}
