using Microsoft.Extensions.Configuration;
using FSL.DTOs; 

namespace FSL.Utilities
{
    static public class Utility
    {
        static public void SetAppSettings(IConfiguration configuration, TestaScontrinoDTO testaScontrino)
        {
            if (configuration == null || testaScontrino == null) return;

            string nomenegozio = configuration["ConfigurazioneCliente:Nome"];
            if (!string.IsNullOrWhiteSpace(nomenegozio))
            {
                testaScontrino.NomeNegozio = nomenegozio;

            }
            string nomecitta = configuration["ConfigurazioneCliente:Citta"];

            if (!string.IsNullOrWhiteSpace(nomecitta))
            {
                testaScontrino.NomeCitta = nomecitta;
            }
            string numerotelefono = configuration["ConfigurazioneCliente:NumeroDiTelefono"];
            if (!string.IsNullOrWhiteSpace(numerotelefono))
            {
                testaScontrino.NumeroTelefono = numerotelefono;
            }
            string partitaiva = configuration["ConfigurazioneCliente:PartitaIVA"];
            if (!string.IsNullOrWhiteSpace(partitaiva))
            {
                testaScontrino.PartitaIva = partitaiva;

            }


        }
    }
}
