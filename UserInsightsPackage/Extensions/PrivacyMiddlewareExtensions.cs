using Microsoft.AspNetCore.Builder;
using UserInsightsPackage.Middleware;

namespace UserInsightsPackage.Extensions
{
    public static class PrivacyMiddlewareExtensions
    {
        public static IApplicationBuilder UsePrivacyMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<PrivacyMiddleware>();
        }
    }
}
