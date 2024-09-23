using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace RestProjectRouTeam.Razor.Helpers
{
    public static class ApiHelper
    {
        public static async Task<dynamic> FetchAsync(HttpClient httpClient, string request)
        {
            var response = await httpClient.GetStringAsync(request);
            dynamic result = JsonConvert.DeserializeObject(response);

            return result;
        }

        public static async Task<string> PostAsync<T>(HttpClient httpClient, string endpoint, T requestData)
        {
            var jsonContent = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                var response = await httpClient.PostAsync(endpoint, content);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (Exception e)
            { 
                return $"Post error: {e.Message}";
            }
        } 

        public static string GetApiCookieName(IConfiguration configuration)
        {
            return configuration.GetSection("ApiAuthCookie")?.Value;
        }

        public static string GetApiUrl(IConfiguration configuration)
        {
            return configuration.GetSection("ApiUrl")?.Value;
        }
    }
}
