namespace UserInsightsPackage.Models
{
    public class UserTrackingOptions
    {
        public bool Enabled { get; set; }
        public bool UserAgentTracking { get; set; }
        public bool IpAddressTracking { get; set; }
        public bool PageAccessTracking { get; set; }
        // Add more properties as needed for tracking options

        public UserTrackingOptions()
        {
            // Set default values for the properties
            Enabled = true;
            UserAgentTracking = true;
            IpAddressTracking = true;
            PageAccessTracking = true;
        }
    }
}
