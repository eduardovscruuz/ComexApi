using System.ComponentModel.DataAnnotations.Schema;

namespace ComexApi.Models;

public class VinculoManifestoEscala
{
    public int ManifestoId { get; set; }
    [ForeignKey("ManifestoId")]
    public Manifesto Manifesto { get; set; }


    public int EscalaId { get; set; }
    [ForeignKey("EscalaId")]
    public Escala Escala { get; set; }


    public DateTime? DataVinculacao { get; set; }


}
