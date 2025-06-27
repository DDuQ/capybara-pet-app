using CapybaraPetApp.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;

namespace CapybaraPetApp.Infrastructure;

public static class QueryCommandPipeline
{
    public static IApplicationBuilder AddMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<EventualConsistencyMiddleware>();
    }
}
