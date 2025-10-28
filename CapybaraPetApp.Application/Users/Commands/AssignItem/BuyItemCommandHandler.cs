using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Commands.AssignItem;

public class BuyItemCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<BuyItemCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(BuyItemCommand command, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(command.UserId);

        if (user is null) return UserErrors.NotFound;

        var result = user.BuyItem(command.ItemId);
        userRepository.UpdateAsync(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return result;
    }
}