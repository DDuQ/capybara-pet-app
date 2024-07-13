using Microsoft.Extensions.DependencyInjection;

namespace CapybaraPetApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        services.AddAutoMapper(typeof(DependencyInjection));

        return services;
    }
}
