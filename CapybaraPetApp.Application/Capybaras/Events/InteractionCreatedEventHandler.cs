using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.Common.EventualConsistency;
using CapybaraPetApp.Domain.UserAggregate.Events;
using MediatR;

namespace CapybaraPetApp.Application.Capybaras.Events;

public class InteractionCreatedEventHandler : INotificationHandler<InteractionCreatedEvent>
{
    private readonly ICapybaraRepository _capybaraRepository;

    public InteractionCreatedEventHandler(ICapybaraRepository capybaraRepository)
    {
        _capybaraRepository = capybaraRepository;
    }

    public async Task Handle(InteractionCreatedEvent notification, CancellationToken cancellationToken)
    {
        var capybara = await _capybaraRepository.GetByIdAsync(notification.Interaction.CapybaraId)
            ?? throw new EventualConsistencyException(InteractionCreatedEvent.CapybaraNotFound);

        capybara.AddInteraction(notification.Interaction);
        capybara.Interact(notification.Interaction);

        await _capybaraRepository.UpdateAsync(capybara);
    }
}
