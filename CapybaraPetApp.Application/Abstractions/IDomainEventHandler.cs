using CapybaraPetApp.Domain.Common;

namespace CapybaraPetApp.Application.Abstractions;

public interface IDomainEventHandler<TEvent>
    where TEvent : IDomainEvent
{
    Task Handle(TEvent domainEvent, CancellationToken cancellationToken);
}
