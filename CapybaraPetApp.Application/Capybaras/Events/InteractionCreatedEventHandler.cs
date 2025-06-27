using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.Common.EventualConsistency;
using CapybaraPetApp.Domain.UserAggregate.Events;

namespace CapybaraPetApp.Application.Capybaras.Events;

public class InteractionCreatedEventHandler : IDomainEventHandler<InteractionCreatedEvent>
{
    private readonly ICapybaraRepository _capybaraRepository;

    public InteractionCreatedEventHandler(ICapybaraRepository capybaraRepository)
    {
        _capybaraRepository = capybaraRepository;
    }

    public async Task Handle(InteractionCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        var capybara = await _capybaraRepository.GetByIdAsync(domainEvent.Interaction.CapybaraId)
            ?? throw new EventualConsistencyException(InteractionCreatedEvent.CapybaraNotFound);

        capybara.AddInteraction(domainEvent.Interaction);
        capybara.Interact(domainEvent.Interaction);

        await _capybaraRepository.UpdateAsync(capybara);
    }
}
