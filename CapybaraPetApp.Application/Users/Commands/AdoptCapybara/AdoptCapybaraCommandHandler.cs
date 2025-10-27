using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Repositories;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Commands.AdoptCapybara;

public class AdoptCapybaraCommandHandler : ICommandHandler<AdoptCapybaraCommand, ErrorOr<Success>>
{
    private readonly ICapybaraRepository _capybaraRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public AdoptCapybaraCommandHandler(IUserRepository userRepository, ICapybaraRepository capybaraRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _capybaraRepository = capybaraRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Success>> Handle(AdoptCapybaraCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(command.UserId);

        if (user == null)
            return Error.NotFound(description: "User not found."); //TODO: Add error code to Domain (UserErrors).

        var capybara = await _capybaraRepository.GetByIdAsync(command.CapybaraId);

        if (capybara == null)
            return Error.NotFound(
                description: "Capybara not found."); //TODO: Add error code to Domain (CapybaraErrors).

        user.AdoptCapybara(capybara.Id);
        capybara.SetOwner(user.Id);

        _userRepository.UpdateAsync(user);
        _capybaraRepository.UpdateAsync(capybara);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success;
    }
}