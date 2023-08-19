using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace UserInsightsPackage.Logging
{
    public class EventLogger
    {
        private readonly TelemetryClient _telemetryClient;

        public EventLogger(string instrumentationKey)
        {
            _telemetryClient = new TelemetryClient { InstrumentationKey = instrumentationKey };
        }

        public void LogEvent(string userId, string eventName, string eventDescription)
        {
            var telemetryEvent = new EventTelemetry(eventName)
            {
                Properties = { { "UserId", userId }, { "EventDescription", eventDescription } }
            };
            _telemetryClient.TrackEvent(telemetryEvent);
        }
    }
}
