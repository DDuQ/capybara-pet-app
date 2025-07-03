using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Common;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Commands.UseItem;

public class UseItemCommandHandler : ICommandHandler<UseItemCommand, ErrorOr<Success>>
{
    private readonly IItemRepository _itemRepository;
    private readonly IUserRepository _userRepository;

    public UseItemCommandHandler(IItemRepository itemRepository, IUserRepository userItemRepository)
    {
        _itemRepository = itemRepository;
        _userRepository = userItemRepository;
    }

    public async Task<ErrorOr<Success>> Handle(UseItemCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(command.UserId);

        if (user is null)
        {
            return Error.NotFound(description: "User not found."); //TODO: Add error code to Domain (UserErrors).
        }

        var item = await _itemRepository.GetByIdAsync(command.ItemId);

        if (item is null)
        {
            return Error.NotFound(description: "Item not found."); //TODO: Add error code to Domain (ItemErrors).
        }

        user.UseItemOnCapybara(command.ItemId, command.capybaraId, command.Amount);

        await _itemRepository.UpdateAsync(item);

        return Result.Success;
    }
}
