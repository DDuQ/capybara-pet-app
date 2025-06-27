using CapybaraPetApp.Domain.Common;

namespace CapybaraPetApp.Application.Abstractions;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
}