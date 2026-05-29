using FSL.Entities;
using FSL.DTOs;
namespace FSL.Utilities;

    static public class Converter
    {
        public static VenCassaDTO Convert(venCassa entity)
        {
            if (entity == null)
            {
                return new VenCassaDTO();
            }
            else
            {
                return new VenCassaDTO
                {
                    tipoOperazione = entity.tipoOperazione,
                    numeroCassa = entity.numeroCassa,
                    numeroPunto = entity.numeroPunto,
                    numeroScontrino = entity.numeroScontrino,
                    formaPagamentoContanti = entity.formaPagamentoContanti,
                    formaPagamentoCredito = entity.formaPagamentoCredito,
                    formaPagamentoCarteCredito = entity.formaPagamentoCarteCredito,
                    formaPagamentoAssegno = entity.formaPagamentoAssegno,
                    formaPagamentoBancomat = entity.formaPagamentoBancomat,
                    formaPagamentoTicket = entity.formaPagamentoTicket,
                    formaPagamentoBuoni = entity.formaPagamentoBuoni,
                    formaPagamentoCreditoPromozioneBuoni = entity.formaPagamentoCreditoPromozioneBuoni,
                    formaPagamentoAltro = entity.formaPagamentoAltro,
                    formaPagamentoAnticipato = entity.formaPagamentoAnticipato,
                    formaPagamentoBuonoCeliachia = entity.formaPagamentoBuonoCeliachia,
                    formaPagamentoSeguiraFattura = entity.formaPagamentoSeguiraFattura,
                    formaPagamentoAggiuntiva1 = entity.formaPagamentoAggiuntiva1,
                    formaPagamentoAggiuntiva2 = entity.formaPagamentoAggiuntiva2,
                    formaPagamentoAggiuntiva3 = entity.formaPagamentoAggiuntiva3,
                    formaPagamentoAggiuntiva4 = entity.formaPagamentoAggiuntiva4,
                    formaPagamentoAggiuntiva5 = entity.formaPagamentoAggiuntiva5,
                    formaPagamentoAggiuntiva6 = entity.formaPagamentoAggiuntiva6,
                    formaPagamentoAggiuntiva7 = entity.formaPagamentoAggiuntiva7,
                    formaPagamentoAggiuntiva8 = entity.formaPagamentoAggiuntiva8,
                    formaPagamentoAggiuntiva9 = entity.formaPagamentoAggiuntiva9,
                    formaPagamentoAggiuntiva10 = entity.formaPagamentoAggiuntiva10,
                    formaPagamentoAggiuntiva11 = entity.formaPagamentoAggiuntiva11,
                    formaPagamentoAggiuntiva12 = entity.formaPagamentoAggiuntiva12,
                    formaPagamentoAggiuntiva13 = entity.formaPagamentoAggiuntiva13,
                    formaPagamentoAggiuntiva14 = entity.formaPagamentoAggiuntiva14,
                    formaPagamentoAggiuntiva15 = entity.formaPagamentoAggiuntiva15
                };
            }
        }

        public static RigaScontrinoDTO Convert(RigaScontrino riga)
        {
            return new RigaScontrinoDTO
            {
                Nome = riga.Descrizione,
                Quantita = riga.Quantita,
                Prezzo = riga.Prezzo
            };


        }
        public static TestaScontrinoDTO Convert(TestaScontrino testa)
        {
            return new TestaScontrinoDTO
            {

                NumeroScontrino = testa.NumeroScontrino,
                Totale = testa.Totale,
                Note = testa.Note,
                DataScontrino = testa.DataScontrino,
                NumeroCassa = testa.NumeroCassa,
                NumeroProdotti = 0,
                Cassa = Convert(testa.Cassa)

            };
        }
    }


