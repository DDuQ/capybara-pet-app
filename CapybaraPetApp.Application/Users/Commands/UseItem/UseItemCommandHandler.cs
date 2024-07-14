using CapybaraPetApp.Application.Common;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Users.Commands.UseItem;

public class UseItemCommandHandler : IRequestHandler<UseItemCommand, ErrorOr<Success>>
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

    public async Task<ErrorOr<Success>> Handle(UseItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetByIdAsync(request.ItemId);

        if (item is null)
        {
            return Error.NotFound(description: "Item not found.");
        }

        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            return Error.NotFound(description: "User not found.");
        }

        var capybara = await _capybaraRepository.GetByIdAsync(request.capybaraId);

        if (capybara is null)
        {
            return Error.NotFound(description: "Capybara not found.");
        }

        //TODO: Remove this repeated validation in workflow when EventualErrorExceptions logic is in place.
        if (!user.OwnsCapybara(capybara))
        {
            return Error.Conflict(description: "Capybara is not owned by this user.");
        }

        var userItem = _userItemRepository.GetByIdsAsync(request.UserId, request.ItemId);

        if (userItem is null)
        {
            return Error.NotFound("User does not have this item.");
        }

        item.UseItem(request.UserId, request.capybaraId, request.Amount);
        
        await _itemRepository.UpdateAsync(item);

        return Result.Success;
    }
}
