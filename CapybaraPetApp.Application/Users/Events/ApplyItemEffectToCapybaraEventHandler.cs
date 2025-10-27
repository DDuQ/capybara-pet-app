using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Domain.Common.EventualConsistency;
using CapybaraPetApp.Domain.Common.Mappings;
using CapybaraPetApp.Domain.ItemAggregate.Events;
using ErrorOr;

namespace CapybaraPetApp.Application.Users.Events;

public class ApplyItemEffectToCapybaraEventHandler(
    IUserRepository userRepository,
    IItemRepository itemRepository,
    ICapybaraRepository capybaraRepository,
    IUnitOfWork unitOfWork)
    : IDomainEventHandler<ApplyItemEffectToCapybaraEvent>
{
    public async Task Handle(ApplyItemEffectToCapybaraEvent domainEvent, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(domainEvent.UserId)
                   ?? throw new EventualConsistencyException(ApplyItemEffectToCapybaraEvent.UserDoesNotExist);

        var item = await itemRepository.GetByIdAsync(domainEvent.ItemId)
                   ?? throw new EventualConsistencyException(ApplyItemEffectToCapybaraEvent.ItemDoesNotExist);

        var capybara = await capybaraRepository.GetByIdAsync(domainEvent.CapybaraId)
                       ?? throw new EventualConsistencyException(ApplyItemEffectToCapybaraEvent.CapybaraDoesNotExist);

        var itemUsageResult = user.UserItems
            .FirstOrDefault(it => it.ItemId == domainEvent.ItemId)?
            .Use(domainEvent.Quantity);

        ValidateItemUsage(itemUsageResult);

        var interactionTypeResult = ItemInteractionMapping.TryGetInteractionStrategy(item.ItemDetail.ItemType);

        if (interactionTypeResult.IsError)
            throw new EventualConsistencyException(ApplyItemEffectToCapybaraEvent.InvalidInteractionType,
                interactionTypeResult.Errors);

        var interactionDetailResult = interactionTypeResult.Value.Validate(domainEvent.Quantity);

        if (interactionDetailResult.IsError)
            throw new EventualConsistencyException(ApplyItemEffectToCapybaraEvent.InvalidInteractionDetail,
                interactionDetailResult.Errors);

        user.InteractWithCapybara(domainEvent.CapybaraId);
        capybara.ReactToInteraction(interactionTypeResult.Value, domainEvent.Quantity);

        capybaraRepository.UpdateAsync(capybara);
        itemRepository.UpdateAsync(item);
        userRepository.UpdateAsync(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static void ValidateItemUsage(ErrorOr<Success>? itemUsageResult)
    {
        if (itemUsageResult is null)
            throw new EventualConsistencyException(ApplyItemEffectToCapybaraEvent.ItemHasNotBeenAssigned);

        if (itemUsageResult.Value.IsError)
            throw new EventualConsistencyException(ApplyItemEffectToCapybaraEvent.InsufficientItem);
    }
}