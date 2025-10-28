using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Domain.ItemAggregate;
using CapybaraPetApp.Domain.UserAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Commands.UseItem;

public class UseItemCommandHandler(
    IItemRepository itemRepository,
    IUserRepository userItemRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<UseItemCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(UseItemCommand command, CancellationToken cancellationToken)
    {
        var user = await userItemRepository.GetByIdAsync(command.UserId);

        if (user is null) return UserErrors.NotFound;

        var item = await itemRepository.GetByIdAsync(command.ItemId);

        if (item is null) return ItemErrors.NotFound;

        user.UseItemOnCapybara(command.ItemId, command.CapybaraId, command.Amount);

        itemRepository.UpdateAsync(item);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success;
    }
}