using CapybaraPetApp.Domain.Common;

namespace CapybaraPetApp.Application.Abstractions.CQRS;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
}