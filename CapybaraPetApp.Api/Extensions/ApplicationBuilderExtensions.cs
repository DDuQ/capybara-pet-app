using CapybaraPetApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CapybaraPetApp.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<CapybaraPetAppDbContext>();
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
    }
}