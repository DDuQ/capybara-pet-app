using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Abstractions.CQRS;
using CapybaraPetApp.Application.Auth;
using CapybaraPetApp.Application.Auth.Utils;
using CapybaraPetApp.Domain.Common.JoinTables.Interaction.Strategies;
using CapybaraPetApp.Domain.ItemAggregate;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CapybaraPetApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
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
        services.AddScoped<IAuthService, AuthService>();
        services.AddKeyedScoped<IInteractionStrategy, FeedStrategy>(ItemType.Fruit);
        services.AddKeyedScoped<IInteractionStrategy, BathStrategy>(ItemType.CleaningTool);
        services.AddKeyedScoped<IInteractionStrategy, PlayStrategy>(ItemType.Toy);
        services.AddOptions<Jwt>().Bind(config.GetSection(nameof(Jwt)));
        return services;
    }
}