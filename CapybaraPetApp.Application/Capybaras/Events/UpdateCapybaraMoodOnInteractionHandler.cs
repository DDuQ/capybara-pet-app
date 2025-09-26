using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.Common.EventualConsistency;
using CapybaraPetApp.Domain.UserAggregate.Events;

namespace CapybaraPetApp.Application.Capybaras.Events;

public class UpdateCapybaraMoodOnInteractionHandler : IDomainEventHandler<UpdateCapybaraMoodOnInteraction>
{
    private readonly ICapybaraRepository _capybaraRepository;

    public UpdateCapybaraMoodOnInteractionHandler(ICapybaraRepository capybaraRepository)
    {
        _capybaraRepository = capybaraRepository;
    }

    public async Task Handle(UpdateCapybaraMoodOnInteraction domainEvent, CancellationToken cancellationToken)
    {
        var capybara = await _capybaraRepository.GetByIdAsync(domainEvent.CapybaraId)
            ?? throw new EventualConsistencyException(UpdateCapybaraMoodOnInteraction.CapybaraNotFound);

        capybara.ReactToInteraction(domainEvent.InteractionStrategy, domainEvent.Quantity);

        await _capybaraRepository.UpdateAsync(capybara);
    }
}
