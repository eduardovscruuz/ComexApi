namespace ComexApi.Models;

public enum StatusEscala
{
    PREVISTA,
    ATRACADA,
    SAIU,
    CANCELADA
}

public class Escala
{
    public int Id { get; set; } // PK
    public required string Navio { get; set; }
    public required string Porto { get; set; }
    public StatusEscala Status { get; set; } // Tipos de status
    public DateTime? ETA { get; set; } // Data Estimada de CHEGADA
    public DateTime? ETB { get; set; } // Data Estimada de ATRACAÇÃO
    public DateTime? ETD { get; set; } // Data Estimada de SAÍDA
    public ICollection<Vinculo>? ManifestosVinculados { get; set; }
}