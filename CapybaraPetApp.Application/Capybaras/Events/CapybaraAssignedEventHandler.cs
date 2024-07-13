using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.UserAggregate.Events;
using MediatR;

namespace CapybaraPetApp.Application.Capybaras.Events;

public class CapybaraAssignedEventHandler : INotificationHandler<CapybaraAssignedEvent>
{
    private readonly ICapybaraRepository _capybaraRepository;

    public CapybaraAssignedEventHandler(ICapybaraRepository capybaraRepository)
    {
        _capybaraRepository = capybaraRepository;
    }

    public async Task Handle(CapybaraAssignedEvent notification, CancellationToken cancellationToken)
    {
        notification.Capybara.AddUserId(notification.UserId);
        await _capybaraRepository.UpdateAsync(notification.Capybara);
    }
}
