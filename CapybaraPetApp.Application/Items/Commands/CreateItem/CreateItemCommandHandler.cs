using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.ItemAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Items.Commands.CreateItem;

public class CreateItemCommandHandler : ICommandHandler<CreateItemCommand, ErrorOr<Guid>>
{
    private readonly IItemRepository _itemRepository;

    public CreateItemCommandHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateItemCommand command, CancellationToken cancellationToken)
    {
        if (await _itemRepository.ExistsByNameAsync(command.Name))
        {
            Error.Conflict(description: $"Item already exists."); //TODO: Add error code to Domain (ItemErrors).
        }

        var item = new Item(command.Name, command.ItemDetail);

        await _itemRepository.AddAsync(item);

        return item.Id;
    }
}
