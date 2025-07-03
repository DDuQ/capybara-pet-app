using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Domain.UserAggregate.Events;

namespace CapybaraPetApp.Application.Capybaras.Events;

public class CapybaraAdoptedEventHandler : IDomainEventHandler<CapybaraAdoptedEvent>
{
    private readonly ICapybaraRepository _capybaraRepository;

    public CapybaraAdoptedEventHandler(ICapybaraRepository capybaraRepository)
    {
        _capybaraRepository = capybaraRepository;
    }

    public Task Handle(CapybaraAdoptedEvent domainEvent, CancellationToken cancellationToken)
    {
        //TODO: Implement the logic to handle the CapybaraAdoptedEvent.
        throw new NotImplementedException();
    }
}
