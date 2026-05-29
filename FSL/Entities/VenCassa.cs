using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FSL.Entities;

[Table("ven_Cassa")]
public class venCassa
{
    [Key]
    [Column("IDCassa")]
    public string IDCassa { get; set; }

    [Column("Tipo operazione")]
    public char tipoOperazione { get; set; }

    [Column("Numero cassa")]
    public int numeroCassa { get; set; }

    [Column("Numero punto")]
    public int numeroPunto { get; set; }
    
    [Column("Numero scontrino")]
    public long numeroScontrino { get; set; }

    [Column("Forma di pagamento - contanti")]
    public decimal formaPagamentoContanti { get; set; }

    [Column("Forma di pagamento - credito")]
    public decimal formaPagamentoCredito { get; set; }

    [Column("Forma di pagamento - carte di credito")]
    public decimal formaPagamentoCarteCredito { get; set; }

    [Column("Forma di pagamento - assegno")]
    public decimal formaPagamentoAssegno { get; set; }

    [Column("Forma di pagamento - bancomat")]
    public decimal formaPagamentoBancomat { get; set; }

    [Column("Forma di pagamento - ticket")]
    public decimal formaPagamentoTicket { get; set; }

    [Column("Forma di pagamento - buoni")]
    public decimal formaPagamentoBuoni { get; set; }

    [Column("Forma di pagamento - credito promozione a buoni")]
    public decimal formaPagamentoCreditoPromozioneBuoni { get; set; }

    [Column("Forma di pagamento - altro")]
    public decimal formaPagamentoAltro { get; set; }

    [Column("Forma di pagamento - anticipato")]
    public decimal formaPagamentoAnticipato { get; set; }

    [Column("Forma di pagamento - buono celiachia")]
    public decimal formaPagamentoBuonoCeliachia { get; set; }

    [Column("Forma di pagamento - seguirà fattura")]
    public decimal formaPagamentoSeguiraFattura { get; set; }


    // Aggiuntive 1..15

    [Column("Forma di pagamento - aggiuntiva 1")]
    public decimal formaPagamentoAggiuntiva1 { get; set; }

    [Column("Forma di pagamento - aggiuntiva 2")]
    public decimal formaPagamentoAggiuntiva2 { get; set; }

    [Column("Forma di pagamento - aggiuntiva 3")]
    public decimal formaPagamentoAggiuntiva3 { get; set; }

    [Column("Forma di pagamento - aggiuntiva 4")]
    public decimal formaPagamentoAggiuntiva4 { get; set; }

    [Column("Forma di pagamento - aggiuntiva 5")]
    public decimal formaPagamentoAggiuntiva5 { get; set; }

    [Column("Forma di pagamento - aggiuntiva 6")]
    public decimal formaPagamentoAggiuntiva6 { get; set; }

    [Column("Forma di pagamento - aggiuntiva 7")]
    public decimal formaPagamentoAggiuntiva7 { get; set; }

    [Column("Forma di pagamento - aggiuntiva 8")]
    public decimal formaPagamentoAggiuntiva8 { get; set; }

    [Column("Forma di pagamento - aggiuntiva 9")]
    public decimal formaPagamentoAggiuntiva9 { get; set; }

    [Column("Forma di pagamento - aggiuntiva 10")]
    public decimal formaPagamentoAggiuntiva10 { get; set; }

    [Column("Forma di pagamento - aggiuntiva 11")]
    public decimal formaPagamentoAggiuntiva11 { get; set; }

    [Column("Forma di pagamento - aggiuntiva 12")]
    public decimal formaPagamentoAggiuntiva12 { get; set; }

    [Column("Forma di pagamento - aggiuntiva 13")]
    public decimal formaPagamentoAggiuntiva13 { get; set; }

    [Column("Forma di pagamento - aggiuntiva 14")]
    public decimal formaPagamentoAggiuntiva14 { get; set; }

    [Column("Forma di pagamento - aggiuntiva 15")]
    public decimal formaPagamentoAggiuntiva15 { get; set; }
}