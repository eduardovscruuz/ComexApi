namespace ComexApi.Models;

public enum TipoManifesto
{
    IMPORTACAO,
    EXPORTACAO
}

public class Manifesto
{
    public int Id { get; set; } // PK

    public required string Numero { get; set; }

    public TipoManifesto Tipo { get; set; }

    public required string Navio { get; set; }

    public required string PortoOrigem { get; set; }

    public required string PortoDestino { get; set; }

    public ICollection<ManifestoEscala> VinculosManifestoEscalas { get; set; } = new List<ManifestoEscala>();
}