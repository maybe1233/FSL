using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace FSL.Pages
{
    public class AssistenteAiModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public AssistenteAiModel(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["Anthropic:ApiKey"] ?? string.Empty;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostChiediClaudeAsync([FromBody] ChatRequest request)
        {
            if (string.IsNullOrEmpty(_apiKey))
            {
                return new BadRequestObjectResult("Errore di configurazione: La chiave 'Anthropic:ApiKey' non è stata trovata.");
            }

            if (string.IsNullOrEmpty(request?.Messaggio))
            {
                return new BadRequestObjectResult("Il messaggio non può essere vuoto.");
            }

            StringBuilder systemPromptBuilder = new StringBuilder();
            systemPromptBuilder.Append("Sei ZEUS, l'assistente virtuale intelligente del sistema di gestione scontrini. Sii professionale, chiaro e cordiale.");

            // GESTIONE 1: Recupero scontrini singoli per ID
            if (request.ScontriniIds != null && request.ScontriniIds.Any())
            {
                systemPromptBuilder.Append("\n\n--- SCONTRINI SPECIFICI RICHIESTI PER ID ---");
                foreach (var id in request.ScontriniIds)
                {
                    try
                    {
                        var response = await _httpClient.GetAsync($"scontrini/scontrino/{id}");
                        if (response.IsSuccessStatusCode)
                        {
                            string json = await response.Content.ReadAsStringAsync();
                            systemPromptBuilder.Append($"\nID Scontrino {id}:\n```json\n{json}\n```\n");
                        }
                    }
                    catch { /* ignoriamo l'errore del singolo per non bloccare tutto */ }
                }
            }

            // GESTIONE 2: Recupero scontrini per Data o Intervallo di tempo
            if (request.DataInizio.HasValue)
            {
                DateTime fine = request.DataFine.HasValue ? request.DataFine.Value : request.DataInizio.Value;
                DateTime inizio = request.DataInizio.Value;

                // Controllo di sicurezza per evitare cicli infiniti o sovraccarichi eccessivi
                if (fine >= inizio && (fine - inizio).TotalDays <= 31)
                {
                    systemPromptBuilder.Append($"\n\n--- SCONTRINI ESTRATTI NEL PERIODO DAL {inizio:dd/MM/yyyy} AL {fine:dd/MM/yyyy} ---");

                    for (DateTime giornoCorrente = inizio; giornoCorrente <= fine; giornoCorrente = giornoCorrente.AddDays(1))
                    {
                        try
                        {
                            // Chiama l'endpoint: scontrini/scontrinigiornata/{anno}/{mese}/{giorno}
                            var urlGiorno = $"scontrini/scontrinigiornata/{giornoCorrente.Year}/{giornoCorrente.Month}/{giornoCorrente.Day}";
                            var response = await _httpClient.GetAsync(urlGiorno);

                            if (response.IsSuccessStatusCode)
                            {
                                string scontriniGiornoJson = await response.Content.ReadAsStringAsync();
                                // Se la lista di scontrini di quella giornata non è vuota []
                                if (scontriniGiornoJson != "[]" && !string.IsNullOrEmpty(scontriniGiornoJson))
                                {
                                    systemPromptBuilder.Append($"\nData {giornoCorrente:dd/MM/yyyy}:\n```json\n{scontriniGiornoJson}\n```\n");
                                }
                            }
                        }
                        catch { /* Salta l'errore del giorno singolo */ }
                    }
                }
                else if ((fine - inizio).TotalDays > 31)
                {
                    systemPromptBuilder.Append("\nNota per l'assistente: L'utente ha chiesto un intervallo superiore a un mese. Di' all'utente che per motivi di prestazioni può selezionare al massimo 31 giorni consecutivi.");
                }
            }

            systemPromptBuilder.Append("\n\nRispondi alle domande ed effettua calcoli, somme, statistiche o confronti basandoti unicamente sui dati strutturati forniti sopra.");

            // Invio ad Anthropic Claude
            try
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://api.anthropic.com/v1/messages");
                httpRequest.Headers.Add("x-api-key", _apiKey);
                httpRequest.Headers.Add("anthropic-version", "2023-06-01");

                var payload = new
                {
                    model = "claude-sonnet-4-20250514",
                    max_tokens = 1024,
                    system = systemPromptBuilder.ToString(),
                    messages = new[] { new { role = "user", content = request.Messaggio } }
                };

                httpRequest.Content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
                var response = await _httpClient.SendAsync(httpRequest);

                if (!response.IsSuccessStatusCode)
                {
                    var errorDetails = await response.Content.ReadAsStringAsync();
                    return new ContentResult { StatusCode = (int)response.StatusCode, Content = errorDetails };
                }

                var responseString = await response.Content.ReadAsStringAsync();
                return new ContentResult { Content = responseString, ContentType = "application/json" };
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult($"Errore interno: {ex.Message}");
            }
        }
    }

    // Modello DTO aggiornato per accogliere sia gli ID che le date dal frontend
    public class ChatRequest
    {
        public string Messaggio { get; set; } = string.Empty;
        public List<long> ScontriniIds { get; set; } = new List<long>();
        public DateTime? DataInizio { get; set; }
        public DateTime? DataFine { get; set; }
    }
}