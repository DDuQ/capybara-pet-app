using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Domain.Common;
using Microsoft.Extensions.DependencyInjection;

namespace CapybaraPetApp.Application.Abstractions;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public DomainEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        var eventType = domainEvent.GetType();
        var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(eventType);
        var handlers = _serviceProvider.GetServices(handlerType);

        foreach (var handler in handlers)
        {
            var method = handlerType.GetMethod("Handle");
            if (method != null) await (Task)method.Invoke(handler, [domainEvent, cancellationToken])!;
        }
    }
}