using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.ItemAggregate;
using ErrorOr;
using MediatR;

namespace CapybaraPetApp.Application.Items.Commands.CreateItem;

public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, ErrorOr<Item>>
{
    private readonly IItemRepository _itemRepository;

    public CreateItemCommandHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task<ErrorOr<Item>> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        if (await _itemRepository.ExistsByNameAsync(request.Name))
        {
            Error.Conflict(description: $"Item already exists.");
        }

        var item = new Item(request.Name, request.ItemDetail);

        await _itemRepository.AddAsync(item);

        return item;
    }
}
