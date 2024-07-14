using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.UserAggregate.Events;
using MediatR;

namespace CapybaraPetApp.Application.Items.Events;

public class ItemAssignedEventHandler : INotificationHandler<ItemAssignedEvent>
{
    private readonly IItemRepository _itemRepository;


    public ItemAssignedEventHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task Handle(ItemAssignedEvent notification, CancellationToken cancellationToken)
    {
        notification.Item.AssignUser(notification.User);
        await _itemRepository.UpdateAsync(notification.Item);
    }
}
