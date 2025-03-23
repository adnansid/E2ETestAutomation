using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace E2ETestAutomation.Utilities
{
    public class TestData
    {
        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;

        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
    }

    // Helper class to load test data from a JSON file.
    public static class TestDataHelper
    {
        /// <summary>
        /// Loads test data from the specified JSON file.
        /// </summary>
        /// <param name="jsonPath">The full file path to the JSON file containing test data.</param>
        /// <returns>An array of TestData objects.</returns>
        public static async Task<TestData[]> LoadTestDataAsync(string jsonPath)
        {
            if (!File.Exists(jsonPath))
            {
                throw new FileNotFoundException($"Test data file not found at {jsonPath}");
            }

            var jsonContent = await File.ReadAllTextAsync(jsonPath);
            var testData = JsonSerializer.Deserialize<TestData[]>(jsonContent);

            if (testData == null || testData.Length == 0)
            {
                throw new InvalidOperationException("Failed to deserialize test data or the file is empty.");
            }

            return testData;
        }
    }
}