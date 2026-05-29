namespace FrontEnd.DTOs;

public class TestaScontrinoDTO
{
    public long NumeroScontrino { get; set; }

    public DateTime DataScontrino { get; set; } = DateTime.Now;

    public decimal NumeroProdotti { get; set; }

    public decimal Totale { get; set; }

    public string? NomeNegozio { get; set; }

    public string? NomeCitta { get; set; }

    public string? NumeroTelefono { get; set; }

    public string? PartitaIva { get; set; }

    public string MetodoPagamento { get; set; } = "Contanti";

    public string? IvaTotale { get; set; }

    public string TipologiaCommerciante { get; set; } = "Generico";

    public int NumeroCassa { get; set; }

    public string? Note { get; set; }

    //public VenCassaDTO Cassa { get; set; } = new VenCassaDTO();

    //public List<RigaScontrinoDTO> Prodotti { get; set; } = new();
}