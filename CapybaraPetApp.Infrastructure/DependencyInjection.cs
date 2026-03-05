using Azure.Storage.Blobs;
using CapybaraPetApp.Application.Abstractions;
using CapybaraPetApp.Application.Abstractions.Clients;
using CapybaraPetApp.Application.Abstractions.Repositories;
using CapybaraPetApp.Infrastructure.Clients;
using CapybaraPetApp.Infrastructure.Common;
using CapybaraPetApp.Infrastructure.Persistence;
using CapybaraPetApp.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CapybaraPetApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);

        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStrings = configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>();
        services.AddSingleton<IDbConnectionFactory>(_ => new DbConnectionFactory(connectionStrings!.SQLServerDb));
        services.AddDbContext<CapybaraPetAppDbContext>(options =>
            options.UseSqlServer(connectionStrings!.SQLServerDb));

        services.AddScoped<ICapybaraRepository, CapybaraRepository>();
        services.AddScoped<IAchievementRepository, AchievementRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IUserItemRepository, UserItemRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAuthTokenRepository, AuthTokenRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IAzureBlobClient, AzureBlobClient>();

        services.AddScoped(_ => new BlobServiceClient(connectionStrings!.CapybuddyStorage));

        return services;
    }
}