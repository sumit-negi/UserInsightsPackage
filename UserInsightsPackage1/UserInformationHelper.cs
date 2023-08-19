using Microsoft.AspNetCore.Http;
using System;
using System.Text.RegularExpressions;
using UserInsightsPackage.Models;

namespace UserInsightsPackage.Helpers
{
    public static class UserInformationHelper
    {
        public static string GetIpAddress(HttpContext context)
        {
            // Get user IP address from HttpContext
            return context.Connection.RemoteIpAddress?.ToString();
        }
        public static BrowserInfo ParseUserAgentForBrowser(string userAgent)
        {
            var browserInfo = new BrowserInfo();

            // Detect browser name and version using regular expressions
            if (!string.IsNullOrEmpty(userAgent))
            {
                var browserRegex = new Regex(@"(MSIE|Trident|Edge|Chrome|Safari|Firefox)/(\d+)");
                var match = browserRegex.Match(userAgent);

                if (match.Success && match.Groups.Count >= 3)
                {
                    browserInfo.Name = match.Groups[1].Value;
                    browserInfo.Version = match.Groups[2].Value;
                }
            }

            return browserInfo;
        }
        // Add this method to your UserTrackingMiddleware.cs file or a relevant class

        public static string ParseUserAgentForOS(string userAgent)
        {
            if (!string.IsNullOrEmpty(userAgent))
            {
                if (userAgent.Contains("Windows NT"))
                {
                    return "Windows";
                }
                else if (userAgent.Contains("Mac OS X"))
                {
                    return "macOS";
                }
                else if (userAgent.Contains("Linux") || userAgent.Contains("Ubuntu"))
                {
                    return "Linux";
                }
                // Add more conditions for other operating systems as needed
            }

            return "Unknown";
        }
        // Add this method to your UserTrackingMiddleware.cs file or a relevant class

        public static DeviceInfo ParseUserAgentForDevice(string userAgent)
        {
            var deviceInfo = new DeviceInfo();

            if (!string.IsNullOrEmpty(userAgent))
            {
                if (userAgent.Contains("iPhone") || userAgent.Contains("iPad"))
                {
                    deviceInfo.Type = "Mobile";
                    deviceInfo.Model = userAgent.Contains("iPhone") ? "iPhone" : "iPad";
                }
                else if (userAgent.Contains("Android"))
                {
                    deviceInfo.Type = "Mobile";
                    deviceInfo.Model = "Android Device";
                }
                else if (userAgent.Contains("Windows Phone"))
                {
                    deviceInfo.Type = "Mobile";
                    deviceInfo.Model = "Windows Phone";
                }
                else if (userAgent.Contains("Windows"))
                {
                    deviceInfo.Type = "PC";
                    deviceInfo.Model = "Windows PC";
                }
                // Add more conditions for other devices as needed
            }

            return deviceInfo;
        }

        // Add this method to your UserTrackingMiddleware.cs file or a relevant class
        public static string GetScreenResolution(HttpContext context)
        {
            // Retrieve screen resolution from request headers or other available data
            string screenWidth = context.Request.Headers["Screen-Width"].ToString();
            string screenHeight = context.Request.Headers["Screen-Height"].ToString();

            return $"{screenWidth}x{screenHeight}";
        }

        // Other helper methods for user information
    }
}
