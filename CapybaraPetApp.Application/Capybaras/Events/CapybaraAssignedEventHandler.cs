using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.UserAggregate.Events;

namespace CapybaraPetApp.Application.Capybaras.Events;

public class CapybaraAssignedEventHandler : IDomainEventHandler<CapybaraAssignedEvent>
{
    private readonly ICapybaraRepository _capybaraRepository;

    public CapybaraAssignedEventHandler(ICapybaraRepository capybaraRepository)
    {
        _capybaraRepository = capybaraRepository;
    }

    public Task Handle(CapybaraAssignedEvent domainEvent, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
