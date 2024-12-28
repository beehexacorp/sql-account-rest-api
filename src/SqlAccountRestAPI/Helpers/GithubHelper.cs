using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace SqlAccountRestAPI.Helpers
{
    public class GithubHelper
    {
        static public async Task<IDictionary<string, object>> GetLatestReleaseInfo()
        {
            string url = $"https://api.github.com/repos/{ApplicationConstants.GITHUB_SQL_ACCOUNT_REST_API_RELEASE_URL}latest";
            using HttpClient client = new HttpClient();

            // Add a User-Agent header required by GitHub API
            client.DefaultRequestHeaders.Add("User-Agent", "SqlAccountRestAPIMonitor");

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                // Deserialize JSON response
                string json = await response.Content.ReadAsStringAsync();
                var release = JsonSerializer.Deserialize<Dictionary<string, object>>(json);

                // Extract "tag_name" from response
                return release ?? new Dictionary<string, object>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching latest version: {ex.Message}");
                return new Dictionary<string, object>
                    {
                        { "Error", ex.Message }
                    };
            }
        }
        static public async Task<string> GetDownloadUrl()
        {
            try
            {
                // Call to GitHub API to fetch latest release information
                var releaseInfo = await GetLatestReleaseInfo();

                if (!releaseInfo.ContainsKey("assets")) return "";
                var assets = (JsonElement)releaseInfo["assets"];
                if (assets.ValueKind != JsonValueKind.Array) return "";


                // Iterate through each asset object in the array
                foreach (var asset in assets.EnumerateArray())
                {
                    if (asset.TryGetProperty("name", out var nameProp) &&
                        asset.TryGetProperty("browser_download_url", out var urlProp) &&
                        nameProp.GetString()?.EndsWith("win-x64.zip") == true)
                    {
                        return urlProp.GetString() ?? "";
                    }
                }

                return "";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching release: {ex.Message}");
                return ex.Message;
            }
        }
    }
}