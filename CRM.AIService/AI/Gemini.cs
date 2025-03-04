using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CRM.AIService.AI
{
    public class Gemini
    {
        private readonly HttpClient _httpClient;

        public Gemini(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetResponseAsync(string apiKey, string combinedPrompt)
        {
            var requestData = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = combinedPrompt }
                        }
                    }
                }
            };

            var requestBody = new StringContent(JsonSerializer.Serialize(requestData), Encoding.UTF8, "application/json");
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={apiKey}";

            var response = await _httpClient.PostAsync(url, requestBody);
            var responseString = await response.Content.ReadAsStringAsync();

            // ✅ Provera i parsiranje odgovora
            try
            {
                var jsonResponse = JsonNode.Parse(responseString);

                // ✅ Ako postoji greška, vraćamo je
                if (jsonResponse?["error"]?["message"] != null)
                {
                    return $"Greška iz API-ja: {jsonResponse["error"]["message"]}";
                }

                // ✅ Parsiranje odgovora - uzimamo generisan tekst
                string rawResponse = jsonResponse?["candidates"]?[0]?["content"]?["parts"]?[0]?["text"]?.ToString() ?? "Greška pri parsiranju odgovora.";

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
