using CapybaraPetApp.Application.Common;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Users.Commands.AddItemCommand;

public class AddItemCommandHandler : IRequestHandler<AddItemCommand, ErrorOr<Success>>
{
    private readonly IUserRepository _userRepository;
    private readonly IItemRepository _itemRepository;

    public AddItemCommandHandler(IUserRepository userRepository, IItemRepository itemRepository)
    {
        _userRepository = userRepository;
        _itemRepository = itemRepository;
    }

    public async Task<ErrorOr<Success>> Handle(AddItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetByIdAsync(request.ItemId);

        if (item is null)
        {
            return Error.NotFound(description: "Item does not exists.");
        }

        var user = await _userRepository.GetByIdAsync(request.UserId);

        if (user is null)
        {
            return Error.NotFound(description: "Item does not exists.");
        }

        user.AddItem(item);

        return Result.Success;
    }
}
