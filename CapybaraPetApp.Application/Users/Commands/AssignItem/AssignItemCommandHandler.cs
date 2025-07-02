using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Common;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Commands.AssignItem;

public class AssignItemCommandHandler : ICommandHandler<AssignItemCommand, ErrorOr<Success>>
{
    private readonly IUserRepository _userRepository;
    private readonly IItemRepository _itemRepository;

    public AssignItemCommandHandler(IUserRepository userRepository, IItemRepository itemRepository)
    {
        _userRepository = userRepository;
        _itemRepository = itemRepository;
    }

    public async Task<ErrorOr<Success>> Handle(AssignItemCommand command, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetByIdAsync(command.ItemId);

        if (item is null)
        {
            return Error.NotFound(description: "Item does not exists."); //TODO: Add error code to Domain (ItemErrors).
        }

        var user = await _userRepository.GetByIdAsync(command.UserId);

        if (user is null)
        {
            return Error.NotFound(description: "Item does not exists."); //TODO: Add error code to Domain (ItemErrors).
        }

        user.AssignItem(item);

        return Result.Success;
    }
}
