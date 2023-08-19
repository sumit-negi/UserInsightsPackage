using System;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;

namespace UserInsightsPackage.Services
{
    public class UserEventService
    {
        private readonly TelemetryClient _telemetryClient;

        public UserEventService(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        public void LogUserEvent(string userId, string eventName, string eventDescription)
        {
            var telemetryEvent = new EventTelemetry(eventName)
            {
                Properties = { { "UserId", userId }, { "EventDescription", eventDescription } }
            };
            _telemetryClient.TrackEvent(telemetryEvent);
        }

        // You can add more methods to log different types of user events
    }
}
