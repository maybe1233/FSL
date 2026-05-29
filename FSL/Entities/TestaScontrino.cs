using FSL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSL.Entities;

[Table("sto_TestaScontrini")]
public class TestaScontrino
{
    [Key]
    [Column("Numero scontrino")]
    public long NumeroScontrino { get; set; }

    [Column("Data creazione")]
    public DateTime DataScontrino { get; set; }

    [Column("Totale")]
    public decimal Totale { get; set; }

    [Column("Note")]
    public string? Note { get; set; }

    [Column("Numero cassa")]
    public int NumeroCassa { get; set; }


    // Navigation properties
    [NotMapped]
    public venCassa? Cassa { get; set; } = null;

    public IEnumerable<RigaScontrino>? RigheScontrino { get; set; }
}