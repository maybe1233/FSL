using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSL.Entities
{
    [Table("Testa Scontrini")]
    public class TestaScontrino
    {
        [Key]
        [Column("Id Scontrino")]
        public int IdScontrino { get; set; }

        [Column("Numero Scontr")]
        [StringLength(50)]
        public string NumeroScontrino { get; set; }

        [Column("Data Movimento")]
        public DateTime DataMovimento { get; set; }

        [Column("Importo Totale")]
        public decimal ImportoTotale { get; set; }

        [Column("Id Cassa")]
        public int IdCassa { get; set; }
    }
}
