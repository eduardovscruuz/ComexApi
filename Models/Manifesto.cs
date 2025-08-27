namespace ComexApi.Models;

public enum TiposManifesto
{
    IMPORTACAO,
    EXPORTACAO
}

public class Manifesto
{
    public int Id { get; set; } // PK

    public required string Numero { get; set; }

    public TiposManifesto Tipo { get; set; }

    public required string Navio { get; set; }

    public required string PortoOrigem { get; set; }

    public required string PortoDestino { get; set; }

    public ICollection<Vinculo>? EscalasVinculadas { get; set; }
}