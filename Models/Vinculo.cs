using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ComexApi.Models;

public class Vinculo
{
    public int ManifestoId { get; set; }
    [ForeignKey("ManifestoId")]
    [JsonIgnore]
    public Manifesto? Manifesto { get; set; }


    public int EscalaId { get; set; }
    [ForeignKey("EscalaId")]
    [JsonIgnore]
    public Escala? Escala { get; set; }


    public DateTime? DataVinculacao { get; set; }


}
