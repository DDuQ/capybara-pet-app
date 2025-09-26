using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.Common.EventualConsistency;
using CapybaraPetApp.Domain.Common.JoinTables.Interaction;
using CapybaraPetApp.Domain.Common.Mappings;
using CapybaraPetApp.Domain.ItemAggregate.Events;

namespace CapybaraPetApp.Application.Users.Events;

public class ApplyItemEffectToCapybaraEventHandler : IDomainEventHandler<ApplyItemEffectToCapybaraEvent>
{
    private readonly IUserRepository _userRepository;
    private readonly IItemRepository _itemRepository;

    public ApplyItemEffectToCapybaraEventHandler(
        IUserRepository userRepository,
        IItemRepository itemRepository)
    {
        _userRepository = userRepository;
        _itemRepository = itemRepository;
    }

    public async Task Handle(ApplyItemEffectToCapybaraEvent domainEvent, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(domainEvent.UserId)
            ?? throw new EventualConsistencyException(ApplyItemEffectToCapybaraEvent.UserDoesNotExist);

        var item = await _itemRepository.GetByIdAsync(domainEvent.ItemId)
            ?? throw new EventualConsistencyException(ApplyItemEffectToCapybaraEvent.ItemDoesNotExist);

        var itemUsageResult = item.Use(domainEvent.Quantity);

        if (itemUsageResult.IsError)
        {
            throw new EventualConsistencyException(ApplyItemEffectToCapybaraEvent.InsufficientItem,
                itemUsageResult.Errors);
        }

        var interactionTypeResult = ItemInteractionMapping.TryGetInteractionStrategy(item.ItemDetail.ItemType);

        if (interactionTypeResult.IsError)
        {
            throw new EventualConsistencyException(ApplyItemEffectToCapybaraEvent.InvalidInteractionType, interactionTypeResult.Errors);
        }

        var interactionDetailResult = interactionTypeResult.Value.Validate(domainEvent.Quantity);

        if (interactionDetailResult.IsError)
        {
            throw new EventualConsistencyException(ApplyItemEffectToCapybaraEvent.InvalidInteractionDetail, interactionDetailResult.Errors);
        }

        user.InteractWithCapybara(domainEvent.CapybaraId, interactionTypeResult.Value, domainEvent.Quantity);

        await _userRepository.UpdateAsync(user);
    }
}
