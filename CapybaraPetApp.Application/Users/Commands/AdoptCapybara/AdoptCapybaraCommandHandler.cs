using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Repositories;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Commands.AdoptCapybara;

public class AdoptCapybaraCommandHandler(
    IUserRepository userRepository,
    ICapybaraRepository capybaraRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<AdoptCapybaraCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(AdoptCapybaraCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(command.UserId);

        if (user == null)
            return Error.NotFound(description: "User not found."); //TODO: Add error code to Domain (UserErrors).

        var capybara = await capybaraRepository.GetByIdAsync(command.CapybaraId);

        if (capybara == null)
            return Error.NotFound(
                description: "Capybara not found."); //TODO: Add error code to Domain (CapybaraErrors).

        user.AdoptCapybara(capybara.Id);
        capybara.SetOwner(user.Id);

        userRepository.UpdateAsync(user);
        capybaraRepository.UpdateAsync(capybara);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success;
    }
}