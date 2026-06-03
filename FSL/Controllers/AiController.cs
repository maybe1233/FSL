using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace FSL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AiController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public AiController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            // Recupera la chiave impostata nel file appsettings.json
            _apiKey = configuration["Anthropic:ApiKey"] ?? throw new ArgumentNullException("Anthropic API Key mancante.");
        }

        [HttpPost("chat")]
        public async Task<IActionResult> ChiediAClaude([FromBody] ChatRequest request)
        {
            if (string.IsNullOrEmpty(request.Messaggio))
            {
                return BadRequest("Il messaggio non può essere vuoto.");
            }

            try
            {
                // Prepara la richiesta HTTP per l'endpoint ufficiale di Anthropic
                var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://api.anthropic.com/v1/messages");
                httpRequest.Headers.Add("x-api-key", _apiKey);
                httpRequest.Headers.Add("anthropic-version", "2023-06-01"); // Versione API richiesta da Anthropic

                // Corpo della richiesta (Payload)
                var payload = new
                {
                    model = "claude-3-5-sonnet-20241022",
                    max_tokens = 1024,
                    messages = new[]
                    {
                        new { role = "user", content = request.Messaggio }
                    }
                };

                httpRequest.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

                // Invia la richiesta
                var response = await _httpClient.SendAsync(httpRequest);

                if (!response.IsSuccessStatusCode)
                {
                    var errorDetails = await response.Content.ReadAsStringAsync();
                    return StatusCode((int)response.StatusCode, $"Errore da Anthropic: {errorDetails}");
                }

                var responseString = await response.Content.ReadAsStringAsync();

                // Restituisce direttamente il JSON arrivato da Claude al tuo frontend
                return Content(responseString, "application/json");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Errore interno del server: {ex.Message}");
            }
        }
    }

    // Modello per ricevere la stringa dal frontend
    public class ChatRequest
    {
        public string Messaggio { get; set; } = string.Empty;
    }
}