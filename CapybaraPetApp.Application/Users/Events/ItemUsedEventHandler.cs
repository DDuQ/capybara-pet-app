using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.Common.EventualConsistency;
using CapybaraPetApp.Domain.Common.JoinTables.Interaction;
using CapybaraPetApp.Domain.ItemAggregate.Events;
using MediatR;

namespace CapybaraPetApp.Application.Users.Events;

public class ItemUsedEventHandler : INotificationHandler<ItemUsedEvent>
{
    private readonly IUserRepository _userRepository;
    private readonly ICapybaraRepository _capybaraRepository;
    private readonly IUserItemRepository _userItemRepository;

    public ItemUsedEventHandler(
        IUserRepository userRepository,
        ICapybaraRepository capybaraRepository,
        IUserItemRepository userItemRepository)
    {
        _userRepository = userRepository;
        _capybaraRepository = capybaraRepository;
        _userItemRepository = userItemRepository;
    }

    public async Task Handle(ItemUsedEvent notification, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(notification.UserId)
            ?? throw new EventualConsistencyException(ItemUsedEvent.UserDoesNotExist);

        var capybara = await _capybaraRepository.GetByIdAsync(notification.CapybaraId)
            ?? throw new EventualConsistencyException(ItemUsedEvent.CapybaraDoesNotExist);

        if (!user.OwnsCapybara(capybara))
        {
            throw new EventualConsistencyException(ItemUsedEvent.CapybaraIsNotOwnedByThisUser);
        }

        var interactionDetailResult = InteractionDetail.IsInteractionDetailValid(notification.InteractionDetail);

        if (interactionDetailResult.IsError)
        {
            throw new EventualConsistencyException(ItemUsedEvent.InvalidInteractionDetail, interactionDetailResult.Errors);
        }

        var interaction = new Interaction(notification.InteractionDetail, notification.UserId, notification.CapybaraId);

        user.AddInteraction(interaction);

        await _userItemRepository.UpdateAsync(notification.UserItem);
        await _userRepository.UpdateAsync(user);
    }
}
