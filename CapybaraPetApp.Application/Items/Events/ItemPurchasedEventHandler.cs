using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Repositories;
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
        //TODO: Re-purpose this logic -- Add a balance to the user and discount the value of said item purchased.
        var item = await _itemRepository.GetByIdAsync(domainEvent.ItemId)
                   ?? throw new EventualConsistencyException(ItemPurchasedEvent.ItemDoesNotExist);
    }
}