using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CRM.AIService.AI
{
    public class DeepSeek
    {
        private readonly HttpClient _httpClient;

        public DeepSeek(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetResponseAsync(string apiKey, string combinedPrompt)
        {
            var requestData = new
            {
                model = "deepseek-chat",
                messages = new[]
                {
                    new { role = "system", content = "You are a helpful assistant." }, // Sistemsku ulogu dodajemo
                    new { role = "user", content = combinedPrompt } // Prompt korisnika
                },
                stream = false
            };

            var requestBody = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

            var response = await _httpClient.PostAsync("https://api.deepseek.com/chat/completions", requestBody);
            var responseString = await response.Content.ReadAsStringAsync();

            // ✅ Provera i parsiranje odgovora
            try
            {
                var jsonResponse = JsonNode.Parse(responseString);

                // ✅ Ako postoji `error_msg`, vraćamo grešku
                if (jsonResponse?["error_msg"] != null)
                {
                    return $"Greška iz API-ja: {jsonResponse["error_msg"]}";
                }

                // ✅ Parsiranje odgovora - uzimamo `content`
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
