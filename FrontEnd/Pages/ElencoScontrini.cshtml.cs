using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontEnd.DTOs; // Assicurati che questo namespace sia corretto per i tuoi DTO
using System.Net.Http.Json;

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
            }
            catch (Exception ex)
            {
                Errore = $"Impossibile recuperare i dati: {ex.Message}";
            }

            return Page();
        }
    }
}