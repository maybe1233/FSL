using FSL.DTOs;
using FSL.Entities;

namespace FSL.utilities
{
    public static class Converter
    {
        // 1. Mappatura RigaScontrino -> RigaScontrinoDTO
        public static RigaScontrinoDTO Convert(RigaScontrino riga)
        {
            if (riga == null) return new RigaScontrinoDTO();

            return new RigaScontrinoDTO
            {
                Nome = riga.Descrizione ?? string.Empty,
                Prezzo = riga.Prezzo,
                Quantita = riga.Quantita
            };
        }

        // 2. Mappatura venCassa -> venCassaDTO
        public static VenCassaDTO Convert(venCassa cassa)
        {
            // Specifica: Se cassa == null restituisce new venCassaDTO()
            if (cassa == null)
            {
                return new VenCassaDTO();
            }

            return new VenCassaDTO
            {
                numeroCassa = cassa.numeroCassa,
                tipoOperazione = cassa.tipoOperazione,
                numeroPunto = cassa.numeroPunto,
                numeroScontrino = cassa.numeroScontrino,
                formaPagamentoContanti = cassa.formaPagamentoContanti,
                formaPagamentoCredito = cassa.formaPagamentoCredito,
                formaPagamentoCarteCredito = cassa.formaPagamentoCarteCredito,
                formaPagamentoAssegno = cassa.formaPagamentoAssegno,
                formaPagamentoBancomat = cassa.formaPagamentoBancomat,
                formaPagamentoTicket = cassa.formaPagamentoTicket,
                formaPagamentoBuoni = cassa.formaPagamentoBuoni,
                formaPagamentoCreditoPromozioneBuoni = cassa.formaPagamentoCreditoPromozioneBuoni,
                formaPagamentoAltro = cassa.formaPagamentoAltro,
                formaPagamentoAnticipato = cassa.formaPagamentoAnticipato,
                formaPagamentoBuonoCeliachia = cassa.formaPagamentoBuonoCeliachia,
                formaPagamentoSeguiraFattura = cassa.formaPagamentoSeguiraFattura,
                formaPagamentoAggiuntiva1 = cassa.formaPagamentoAggiuntiva1,
                formaPagamentoAggiuntiva2 = cassa.formaPagamentoAggiuntiva2,
                formaPagamentoAggiuntiva3 = cassa.formaPagamentoAggiuntiva3,
                formaPagamentoAggiuntiva4 = cassa.formaPagamentoAggiuntiva4,
                formaPagamentoAggiuntiva5 = cassa.formaPagamentoAggiuntiva5,
                formaPagamentoAggiuntiva6 = cassa.formaPagamentoAggiuntiva6,
                formaPagamentoAggiuntiva7 = cassa.formaPagamentoAggiuntiva7,
                formaPagamentoAggiuntiva8 = cassa.formaPagamentoAggiuntiva8,
                formaPagamentoAggiuntiva9 = cassa.formaPagamentoAggiuntiva9,
                formaPagamentoAggiuntiva10 = cassa.formaPagamentoAggiuntiva10,
                formaPagamentoAggiuntiva11 = cassa.formaPagamentoAggiuntiva11,
                formaPagamentoAggiuntiva12 = cassa.formaPagamentoAggiuntiva12,
                formaPagamentoAggiuntiva13 = cassa.formaPagamentoAggiuntiva13,
                formaPagamentoAggiuntiva14 = cassa.formaPagamentoAggiuntiva14,
                formaPagamentoAggiuntiva15 = cassa.formaPagamentoAggiuntiva15
            };
        }

        // 3. Mappatura TestaScontrino -> TestaScontrinoDTO
        public static TestaScontrinoDTO Convert(TestaScontrino testa)
        {
            if (testa == null) return new TestaScontrinoDTO();

            var dto = new TestaScontrinoDTO
            {
                NumeroScontrino = testa.NumeroScontrino,
                DataScontrino = testa.DataScontrino,
                Totale = testa.Totale,
                Note = testa.Note,
                NumeroCassa = testa.NumeroCassa,

                // Specifica: NumeroProdotti inizializzato a 0
                NumeroProdotti = 0,

                // Specifica: Chiama Convert(testa.Cassa)
                Cassa = Convert(testa.Cassa)
            };

            // Mappatura della lista delle righe (Corretto senza duplicazioni)
            // NOTA: Verifica se le tue proprietà si chiamano "RigheScontrino" o "RigaScontrino"
            if (testa.RigheScontrino != null)
            {
                foreach (var riga in testa.RigheScontrino)
                {
                    // Se la lista nel DTO si chiama "Prodotti":
                    dto.Prodotti.Add(Convert(riga));

                    // Se invece nel DTO si chiama "RigheScontrino", usa:
                    // dto.RigheScontrino.Add(Convert(riga));
                }
            }

            return dto;
        }
    }
}  