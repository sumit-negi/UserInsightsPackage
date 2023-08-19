using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.Extensions.Options;
using UserInsightsPackage.Helpers;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
using UserInsightsPackage.Models;

namespace UserInsightsPackage.Middleware
{
    public class UserTrackingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly UserTrackingOptions _options;
        private readonly TelemetryClient _telemetryClient;

        public UserTrackingMiddleware(RequestDelegate next, IOptions<UserTrackingOptions> options, TelemetryClient telemetryClient)
        {
            _next = next;
            _options = options.Value;
            _telemetryClient = telemetryClient;
        }

        public async Task Invoke(HttpContext context)
        {
            // Capture user information here using HttpContext and _options
            // Log custom events using EventLogger
            if (_options.Enabled)
            {
                // Capture user information based on _options configuration
                if (_options.UserAgentTracking)
                {
                    string userAgent = context.Request.Headers["User-Agent"];

                    // Extract browser name and version
                    var browserInfo = UserInformationHelper.ParseUserAgentForBrowser(userAgent);
                    LogUserAgent("BrowserName", browserInfo.Name);
                    LogUserAgent("BrowserVersion", browserInfo.Version);

                    // Extract operating system and device details
                    var os = UserInformationHelper.ParseUserAgentForOS(userAgent);
                    LogUserAgent("OperatingSystem", os);
                    var deviceInfo = UserInformationHelper.ParseUserAgentForDevice(userAgent);
                    LogUserAgent("DeviceType", deviceInfo.Type);
                    LogUserAgent("DeviceModel", deviceInfo.Model);

                    // Capture screen resolution
                    var screenResolution = UserInformationHelper.GetScreenResolution(context);
                    LogUserAgent("ScreenResolution", screenResolution);
                }

                // Capture and log user information based on _options configuration

                // Other user tracking logic based on _options
            }

            await _next(context);
        }
        private void LogUserAgent(string propertyName, string propertyValue)
        {
            string eventName = "UserInfoLogged";
            var properties = new Dictionary<string, string>
            {
                { propertyName, propertyValue }
            };
            _telemetryClient.TrackEvent(eventName, properties);
        }
    }
}
