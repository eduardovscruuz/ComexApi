namespace ComexApi.Models;

public enum EStatus
{
    PREVISTA,
    ATRACADA,
    SAIU,
    CANCELADA
}

public class Escala
{
    public int Id { get; set; }// PK
    public required string Navio { get; set; }
    public required string Porto { get; set; }
    public DateTime ETA { get; set; } // Data Estimada de CHEGADA
    public DateTime ETB { get; set; } // Data Estimada de ATRACAÇÃO
    public DateTime ETD { get; set; } // Data Estimada de SAÍDA
    public EStatus Status { get; set; } // Tipos de status

    public ICollection<ManifestoEscala> VinculosManifestoEscalas { get; set; } = new List<ManifestoEscala>();
}