using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Common;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Commands.UseItem;

public class UseItemCommandHandler : ICommandHandler<UseItemCommand, ErrorOr<Success>>
{
    private readonly IUserRepository _userRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IUserItemRepository _userItemRepository;
    private readonly ICapybaraRepository _capybaraRepository;

    public UseItemCommandHandler(
        IUserRepository userRepository,
        IItemRepository itemRepository,
        IUserItemRepository userItemRepository,
        ICapybaraRepository capybaraRepository)
    {
        _userRepository = userRepository;
        _itemRepository = itemRepository;
        _userItemRepository = userItemRepository;
        _capybaraRepository = capybaraRepository;
    }

    public async Task<ErrorOr<Success>> Handle(UseItemCommand command, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetByIdAsync(command.ItemId);

        if (item is null)
        {
            return Error.NotFound(description: "Item not found.");
        }

        var user = await _userRepository.GetByIdAsync(command.UserId);

        if (user is null)
        {
            return Error.NotFound(description: "User not found.");
        }

        var capybara = await _capybaraRepository.GetByIdAsync(command.capybaraId);

        if (capybara is null)
        {
            return Error.NotFound(description: "Capybara not found.");
        }

        //TODO: Remove this repeated validation in workflow when EventualErrorExceptions logic is in place.
        if (!user.OwnsCapybara(capybara))
        {
            return Error.Conflict(description: "Capybara is not owned by this user.");
        }

        var userItem = _userItemRepository.GetByIdsAsync(command.UserId, command.ItemId);

        if (userItem is null)
        {
            return Error.NotFound("User does not have this item.");
        }

        item.UseItem(command.UserId, command.capybaraId, command.Amount);

        await _itemRepository.UpdateAsync(item);

        return Result.Success;
    }
}
