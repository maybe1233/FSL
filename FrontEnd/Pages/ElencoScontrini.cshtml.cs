using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontEnd.DTOs; // Assicurati che questo namespace sia corretto per i tuoi DTO
using System.Net.Http.Json;
using System.Linq;

namespace FrontEnd.Pages
{
    public class ElencoScontriniModel : PageModel
    {
        private readonly HttpClient _http;

        public ElencoScontriniModel(HttpClient http)
        {
            _http = http;
        }

        [BindProperty(SupportsGet = true)]
        public DateTime DataSelezionata { get; set; } = DateTime.Today;

        public List<TestaScontrinoDTO>? ListaScontrini { get; set; }
        public string? Errore { get; set; }

        // Proprietà per i KPI della pagina
        public decimal IncassoTotale { get; set; } = 0.00m;
        public int TotaleScontrini { get; set; } = 0;
        public decimal CarrelloMedio { get; set; } = 0.00m;
        public string MetodoPrevalente { get; set; } = "N/D"; // Nuova proprietà per il KPI richiesto

        public void OnGet()
        {
            // Quando la pagina si carica la prima volta, non fa nulla
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Errore = null;

            try
            {
                // Costruisce la route dinamica verso il backend FSL
                string url = $"http://localhost:5073/Scontrini/scontrinigiornata/{DataSelezionata.Year}/{DataSelezionata.Month:D2}/{DataSelezionata.Day:D2}";

                // Interroga il backend
                ListaScontrini = await _http.GetFromJsonAsync<List<TestaScontrinoDTO>>(url);

                // --- CALCOLO REAL-TIME DEI KPI ---
                if (ListaScontrini != null && ListaScontrini.Count > 0)
                {
                    // 1. Conta il numero totale di scontrini ricevuti
                    TotaleScontrini = ListaScontrini.Count;

                    // 2. Somma i totali di ogni scontrino (usando il campo reale 'Totale')
                    IncassoTotale = ListaScontrini.Sum(s => s.Totale);

                    // 3. Calcola il valore del carrello medio
                    if (TotaleScontrini > 0)
                    {
                        CarrelloMedio = IncassoTotale / TotaleScontrini;
                    }

                    // 4. Calcola il metodo di pagamento prevalente
                    // Raggruppa per metodo, ordina per quanti ce ne sono in ordine decrescente e prende il primo
                    var metodoTop = ListaScontrini
                        .Where(s => !string.IsNullOrEmpty(s.MetodoPagamento))
                        .GroupBy(s => s.MetodoPagamento)
                        .OrderByDescending(g => g.Count())
                        .Select(g => g.Key)
                        .FirstOrDefault();

                    MetodoPrevalente = metodoTop ?? "N/D";
                }
                else
                {
                    // Se la lista è vuota, azzera esplicitamente i contatori
                    ResetKPI();
                }
            }
            catch (Exception ex)
            {
                Errore = $"Impossibile recuperare i dati: {ex.Message}";
                ResetKPI();
            }

            return Page();
        }

        private void ResetKPI()
        {
            TotaleScontrini = 0;
            IncassoTotale = 0.00m;
            CarrelloMedio = 0.00m;
            MetodoPrevalente = "N/D";
        }
    }
}