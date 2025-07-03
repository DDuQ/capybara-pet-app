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

        var interactionTypeResult = ItemInteractionMapping.TryGetInteractionType(item.ItemDetail.ItemType);

        if (interactionTypeResult.IsError)
        {
            throw new EventualConsistencyException(ApplyItemEffectToCapybaraEvent.InvalidInteractionType, interactionTypeResult.Errors);
        }

        var interactionDetail = new InteractionDetail(interactionTypeResult.Value, domainEvent.itemAmount);

        var interactionDetailResult = InteractionDetail.Validate(interactionDetail);

        if (interactionDetailResult.IsError)
        {
            throw new EventualConsistencyException(ApplyItemEffectToCapybaraEvent.InvalidInteractionDetail, interactionDetailResult.Errors);
        }

        user.InteractWithCapybara(domainEvent.CapybaraId, interactionDetail);

        await _itemRepository.UpdateAsync(item);
        await _userRepository.UpdateAsync(user);
    }
}
