using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CRM.AIService.AI
{
    public class ChatGPT
    {
        private readonly HttpClient _httpClient;

        public ChatGPT(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetResponseAsync(string apiKey, string combinedPrompt)
        {
            var requestData = new
            {
                model = "gpt-4",
                messages = new[]
                {
                    new { role = "user", content = combinedPrompt }
                },
                temperature = 0.7,
                max_tokens = 256
            };

            var requestBody = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", requestBody);
            var responseString = await response.Content.ReadAsStringAsync();

            // ✅ Parsiranje JSON odgovora i čišćenje teksta
            try
            {
                var jsonResponse = JsonNode.Parse(responseString);
                string rawResponse = jsonResponse?["choices"]?[0]?["message"]?["content"]?.ToString() ?? "Greška pri parsiranju odgovora.";

                // ✅ Uklanjanje `\n`, `/` i suvišnih razmaka
                string cleanedResponse = Regex.Replace(rawResponse, @"[\n/]", " ").Trim();

                return cleanedResponse;
            }
            catch
            {
                return "Greška pri parsiranju odgovora.";
            }
        }
    }
}
