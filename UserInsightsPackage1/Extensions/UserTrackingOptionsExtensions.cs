using Microsoft.AspNetCore.Builder;
using UserInsightsPackage.Middleware;

namespace UserInsightsPackage.Extensions
{
    public static class UserTrackingMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserTrackingMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<UserTrackingMiddleware>();
        }
    }
}
