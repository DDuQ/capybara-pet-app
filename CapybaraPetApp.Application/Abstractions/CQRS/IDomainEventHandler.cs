using CapybaraPetApp.Domain.Common;

namespace CapybaraPetApp.Application.Abstractions.CQRS;

public interface IDomainEventHandler<TEvent>
    where TEvent : IDomainEvent
{
    Task Handle(TEvent domainEvent, CancellationToken cancellationToken);
}