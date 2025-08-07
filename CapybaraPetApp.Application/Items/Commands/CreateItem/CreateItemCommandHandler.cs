using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.ItemAggregate;
using ErrorOr;

namespace CapybaraPetApp.Application.Items.Commands.CreateItem;

public class CreateItemCommandHandler : ICommandHandler<CreateItemCommand, ErrorOr<Item>>
{
    private readonly IItemRepository _itemRepository;

    public CreateItemCommandHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task<ErrorOr<Item>> Handle(CreateItemCommand command, CancellationToken cancellationToken)
    {
        if (await _itemRepository.ExistsByNameAsync(command.Name))
        {
            return ItemErrors.ItemAlreadyExists;
        }

        var item = new Item(command.Name, command.ItemDetail);

        await _itemRepository.AddAsync(item);

        return item;
    }
}
