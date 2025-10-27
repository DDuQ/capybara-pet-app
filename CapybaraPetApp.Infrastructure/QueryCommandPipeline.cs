using CapybaraPetApp.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;

namespace CapybaraPetApp.Infrastructure;

public static class QueryCommandPipeline
{
    public static IApplicationBuilder UseMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<EventualConsistencyMiddleware>();
    }
}