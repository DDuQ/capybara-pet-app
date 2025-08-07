using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.Common.EventualConsistency;
using CapybaraPetApp.Domain.UserAggregate.Events;

namespace CapybaraPetApp.Application.Items.Events;

public class ItemPurchasedEventHandler : IDomainEventHandler<ItemPurchasedEvent>
{
    private readonly IItemRepository _itemRepository;

    public ItemPurchasedEventHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task Handle(ItemPurchasedEvent domainEvent, CancellationToken cancellationToken)
    {
        //TODO: Re-purpose this logic.
        var item = await _itemRepository.GetByIdAsync(domainEvent.ItemId)
            ?? throw new EventualConsistencyException(ItemPurchasedEvent.ItemDoesNotExist);
    }
}
