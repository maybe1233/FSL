using System.ComponentModel.DataAnnotations.Schema;

namespace FSL.Entities;

[Table("sto_RigheScontrini")]
public class RigaScontrino
{
    [Column("Numero riga")]
    public int NumeroRiga { get; set; }

    [Column("Numero scontrino")]
    public long NumeroScontrino { get; set; }

    [Column("Descrizione")]
    public string? Descrizione { get; set; }

    [Column("Importo")]
    public decimal Prezzo { get; set; }

    [Column("Quantità")]
    public decimal Quantita { get; set; }


    // Navigation property

    public TestaScontrino? TestaScontrino { get; set; }
}