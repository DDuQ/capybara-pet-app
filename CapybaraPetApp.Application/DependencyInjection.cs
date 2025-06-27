using CapybaraPetApp.Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace CapybaraPetApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssembliesOf(typeof(DependencyInjection))
            .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
                .AsImplementedInterfaces()
            .AddClasses(classes => classes.AssignableTo(typeof(IDomainEventHandler<>)))
                .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();

        services.AddAutoMapper(typeof(DependencyInjection));

        return services;
    }
}
