using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Domain.Common;
using CapybaraPetApp.Domain.Common.EventualConsistency;
using CapybaraPetApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;

namespace CapybaraPetApp.Infrastructure.Middleware;

public class EventualConsistencyMiddleware(RequestDelegate next)
{
    public const string DomainEventsKey = "DomainEventsKey";

    public async Task InvokeAsync(HttpContext context, IDomainEventDispatcher dispatcher,
        CapybaraPetAppDbContext dbContext)
    {
        var transaction = await dbContext.Database.BeginTransactionAsync();
        context.Response.OnCompleted(async () =>
        {
            try
            {
                if (context.Items.TryGetValue(DomainEventsKey, out var value) &&
                    value is Queue<IDomainEvent> domainEvents)
                    while (domainEvents.TryDequeue(out var nextEvent))
                        await dispatcher.DispatchAsync(nextEvent);

                await transaction.CommitAsync();
            }
            catch (EventualConsistencyException)
            {
                //todo: Add logic to handle Eventual Consistency Exceptions.
            }
            finally
            {
                await transaction.DisposeAsync();
            }
        });

        await next(context);
    }
}