using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.UserAggregate.Events;

namespace CapybaraPetApp.Application.Items.Events;

public class ItemAssignedEventHandler : IDomainEventHandler<ItemAssignedEvent>
{
    private readonly IItemRepository _itemRepository;


    public ItemAssignedEventHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task Handle(ItemAssignedEvent domainEvent, CancellationToken cancellationToken)
    {
        domainEvent.Item.AssignUser(domainEvent.User);
        await _itemRepository.UpdateAsync(domainEvent.Item);
    }
}
