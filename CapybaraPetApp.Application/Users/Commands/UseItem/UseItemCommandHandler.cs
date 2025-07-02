using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Common;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Commands.UseItem;

public class UseItemCommandHandler : ICommandHandler<UseItemCommand, ErrorOr<Success>>
{
    private readonly IItemRepository _itemRepository;

    public UseItemCommandHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task<ErrorOr<Success>> Handle(UseItemCommand command, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetByIdAsync(command.ItemId);

        if (item is null)
        {
            return Error.NotFound(description: "Item not found."); //TODO: Add error code to Domain (ItemErrors).
        }

        item.UseItem(command.UserId, command.capybaraId, command.Amount);

        await _itemRepository.UpdateAsync(item);

        return Result.Success;
    }
}
