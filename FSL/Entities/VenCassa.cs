using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSL.Entities
{
    [Table("ven Cassa")]
    public class VenCassa
    {
        [Key]
        [Column("Id Cassa")]
        public int IdCassa { get; set; }

        [Column("Numero Cassa")]
        public int NumeroCassa { get; set; }

        [Column("Descrizione")]
        [StringLength(100)]
        public string Descrizione { get; set; }

        [Column("Data Attivazione")]
        public DateTime DataAttivazione { get; set; }
    }
}
