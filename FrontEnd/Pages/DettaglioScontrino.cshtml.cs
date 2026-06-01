using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontEnd.DTOs; // Assicurati che sia il namespace corretto per i tuoi DTO
using System.Net.Http.Json;

namespace FrontEnd.Pages
{
    public class DettaglioScontrinoModel : PageModel
    {
        private readonly HttpClient _http;

        public DettaglioScontrinoModel(HttpClient http)
        {
            _http = http;
        }

        [BindProperty(SupportsGet = true)]
        public string? NumeroScontrinoCercato { get; set; }

        // Supponiamo che tu abbia un DTO specifico per i dettagli o usi lo stesso TestaScontrinoDTO
        // Se hai un DTO tipo 'DettaglioScontrinoDTO', sostituiscilo qui sotto
        public TestaScontrinoDTO? Scontrino { get; set; }

        public string? Errore { get; set; }
        public bool RicercaEffettuata { get; set; } = false;

        public void OnGet()
        {
            // Pagina vuota all'apertura
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(NumeroScontrinoCercato))
            {
                Errore = "Inserisci un numero di scontrino valido.";
                return Page();
            }

            Errore = null;
            RicercaEffettuata = true;

            try
            {
               
                string url = $"http://localhost:5073/Scontrini/scontrino/{NumeroScontrinoCercato}";

                // Interroga il backend
                Scontrino = await _http.GetFromJsonAsync<TestaScontrinoDTO>(url);

                if (Scontrino == null)
                {
                    Errore = $"Scontrino numero {NumeroScontrinoCercato} non trovato.";
                }
            }
            catch (Exception ex)
            {
                Errore = $"Impossibile recuperare i dettagli dello scontrino: {ex.Message}";
                Scontrino = null;
            }

            return Page();
        }
    }
}