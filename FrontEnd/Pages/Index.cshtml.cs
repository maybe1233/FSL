using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace FrontEnd.Pages
{
    public class IndexModel : PageModel
    {
        private readonly HttpClient _http;

        // Proprietà per lo stato della connessione
        public bool IsDatabaseConnesso { get; set; } = false;

        // Proprietà jolly in maiuscolo (per sicurezza)
        public decimal IncassoTotale { get; set; } = 0.00m;
        public int TotaleScontrini { get; set; } = 0;
        public decimal CarrelloMedio { get; set; } = 0.00m;

        // Proprietà jolly in minuscolo (se l'errore le cerca così)
        public decimal incassoTotale { get; set; } = 0.00m;
        public int totaleScontrini { get; set; } = 0;
        public decimal carrelloMedio { get; set; } = 0.00m;

        public IndexModel(HttpClient http)
        {
            _http = http;
        }

        public async Task OnGetAsync()
        {
            using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(2)))
            {
                try
                {
                    var response = await _http.GetAsync("http://localhost:5073/Scontrini/scontrinigiornata/2026/01/01", cts.Token);

                    if (response.IsSuccessStatusCode || (int)response.StatusCode == 404 || (int)response.StatusCode == 204)
                    {
                        IsDatabaseConnesso = true;
                    }
                }
                catch
                {
                    IsDatabaseConnesso = false;
                }
            }
        }
    }
}