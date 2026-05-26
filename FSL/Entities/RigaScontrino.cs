using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSL.Entities
{
    [Table("Righe Scontrini")]
    public class RigaScontrino
    {
        [Key]
        [Column("Id Riga")]
        public int IdRiga { get; set; }

        [Column("Id Scontrino")]
        public int IdScontrino { get; set; }

        [Column("Codice Prodotto")]
        [StringLength(50)]
        public string CodiceProdotto { get; set; }

        [Column("Descrizione Articolo")]
        [StringLength(200)]
        public string DescrizioneArticolo { get; set; }

        [Column("Quantita")]
        public decimal Quantita { get; set; }

        [Column("Prezzo Unitario")]
        public decimal PrezzoUnitario { get; set; }

        [Column("Importo")]
        public decimal Importo { get; set; }
    }
}