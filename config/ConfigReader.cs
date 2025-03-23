using Microsoft.Extensions.Configuration;
using System.IO;

namespace E2ETestAutomation
{
    public static class ConfigReader
    {
        private static readonly IConfigurationRoot _configuration;

        static ConfigReader()
        {
            // Build the configuration from appsettings.json
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config/appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public static string GetBaseUrl()
        {
            return _configuration["BaseUrl"];
        }

        public static bool GetBrowserHeadless()
        {
            // "Headless" is stored as a string in appsettings.json; convert it to bool.
            if (bool.TryParse(_configuration["BrowserConfig:Headless"], out bool result))
                return result;

            return false;
        }

        public static string[] GetBrowserArgs()
        {
            // Retrieve the string array from "BrowserConfig:Args"
            var argsSection = _configuration.GetSection("BrowserConfig:Args");
            return argsSection.Get<string[]>() ?? new string[0];
        }

        public static string GetMySqlConnectionString()
        {
            return _configuration.GetConnectionString("MySqlConnection");
        }
    }
}