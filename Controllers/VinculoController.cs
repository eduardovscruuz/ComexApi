using ComexApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace ComexApi.Controllers;

[ApiController]
[Route("vincular")]
public class VinculoController : ControllerBase
{
    private readonly IVinculoService _service;
    public VinculoController(IVinculoService service) => _service = service;

    [HttpPost("{manifestoId}")]
    public async Task<IActionResult> Vincular(int manifestoId, [FromBody] List<int> escalasIds)
    {
        var resultado = await _service.Vincular(manifestoId, escalasIds);
        if (!resultado) return BadRequest("Falha ao vincular manifesto.");
        return Ok("Manifesto vinculado com sucesso.");
    }

    [HttpDelete("{manifestoId}/{escalaId}")]
    public async Task<IActionResult> Desvincular(int manifestoId, int escalaId)
    {
        if (!await _service.Desvincular(manifestoId, escalaId)) return NotFound();
        return NoContent();
    }

    [HttpGet("manifestos/{manifestoId}")]
    public async Task<IActionResult> EscalasDeManifesto(int manifestoId) =>
        Ok(await _service.ListarEscalasDeManifesto(manifestoId));

    [HttpGet("escalas/{escalaId}")]
    public async Task<IActionResult> ManifestosDeEscala(int escalaId) =>
        Ok(await _service.ListarManifestosDeEscala(escalaId));
}