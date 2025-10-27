using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Domain.ItemAggregate;
using CapybaraPetApp.Domain.UserAggregate;
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

        if (user is null) return UserErrors.NotFound;

        var item = await _itemRepository.GetByIdAsync(command.ItemId);

        if (item is null) return ItemErrors.NotFound;

        user.UseItemOnCapybara(command.ItemId, command.CapybaraId, command.Amount);

        _itemRepository.UpdateAsync(item);

        return Result.Success;
    }
}