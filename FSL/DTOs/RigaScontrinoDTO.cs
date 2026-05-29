namespace FSL.DTOs;

public class RigaScontrinoDTO
{
    public string? Nome { get; set; }

    public decimal Prezzo { get; set; }

    public decimal Quantita { get; set; } = 1;

    public int IvaProdotto { get; set; } = 22;
}