using CapybaraPetApp.Domain.AchievementAggregare;
using CapybaraPetApp.Domain.CapybaraAggregate;
using CapybaraPetApp.Domain.Common;
using CapybaraPetApp.Domain.InteractionAggregate;
using CapybaraPetApp.Domain.ItemAggregate;
using CapybaraPetApp.Domain.UserAggregate;
using CapybaraPetApp.Infrastructure.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CapybaraPetApp.Infrastructure.Persistence;

public class CapybaraPetAppDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CapybaraPetAppDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public DbSet<Capybara> Avatar => Set<Capybara>();
    public DbSet<User> User => Set<User>();
    public DbSet<Item> Item => Set<Item>();
    public DbSet<Interaction> Interaction => Set<Interaction>();
    public DbSet<Achievement> Achievement => Set<Achievement>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        var domainEvents = ChangeTracker.Entries<AggregateRoot>()
           .Select(entry => entry.Entity.PopDomainEvents())
           .SelectMany(x => x)
           .ToList();

        var result = base.SaveChangesAsync(cancellationToken);

        Queue<IDomainEvent> domainEventsQueue = _httpContextAccessor.HttpContext!.Items.TryGetValue(EventualConsistencyMiddleware.DomainEventsKey, out var value) &&
            value is Queue<IDomainEvent> existingDomainEvents
            ? existingDomainEvents
            : [];

        domainEvents.ForEach(domainEventsQueue.Enqueue);
        _httpContextAccessor.HttpContext.Items[EventualConsistencyMiddleware.DomainEventsKey] = domainEventsQueue;

        return result;
    }
}