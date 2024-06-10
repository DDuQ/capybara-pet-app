using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using CapybaraPetApp.Infrastructure.Configurations;
using CapybaraPetApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using CapybaraPetApp.Application.Common;
using CapybaraPetApp.Infrastructure.Persistence.Repositories;
using CapybaraPetApp.Domain.UserAggregate;
using CapybaraPetApp.Domain.AchievementAggregare;
using CapybaraPetApp.Domain.AvatarAggregate;
using CapybaraPetApp.Domain.InteractionAggregate;
using CapybaraPetApp.Domain.ItemAggregate;

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
        //services.Configure<MongoDbSettings>(configuration.GetSection(MongoDbSettings.Section));

        var dbConfigs = new MongoDbSettings();
        configuration.Bind(MongoDbSettings.Section, dbConfigs);

        services.AddDbContext<CapybaraPetAppDbContext>(options => 
            options.UseMongoDB(dbConfigs.ConnectionString, dbConfigs.DatabaseName));

        services.AddScoped<IRepository<Achievement>, AchievementRepository>();
        services.AddScoped<IRepository<Avatar>, AvatarRepository>();
        services.AddScoped<IRepository<Interaction>, InteractionRepository>();
        services.AddScoped<IRepository<Item>, ItemRepository>();
        services.AddScoped<IRepository<User>, UserRepository>();

        return services;
    }
}
