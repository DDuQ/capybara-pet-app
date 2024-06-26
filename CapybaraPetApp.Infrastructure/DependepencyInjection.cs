using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Infrastructure.Common;
using CapybaraPetApp.Infrastructure.Persistence;
using CapybaraPetApp.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CapybaraPetApp.Infrastructure;

public static class DependepencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);

        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var sqlServerDbSettings = configuration.GetSection(SQLServerDbSettings.Section).Get<SQLServerDbSettings>();
        services.Configure<SQLServerDbSettings>(configuration.GetSection(SQLServerDbSettings.Section));

        services.AddDbContext<CapybaraPetAppDbContext>(options =>
        options.UseSqlServer(sqlServerDbSettings!.ConnectionString));

        services.AddScoped<ICapybaraRepository, CapybaraRepository>();
        services.AddScoped<IAchievementRepository, AchievementRepository>();
        services.AddScoped<IInteractionRepository, InteractionRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}