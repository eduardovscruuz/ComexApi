using ComexApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComexApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VinculosController : ControllerBase
{
    private readonly IVinculoService _vinculoService;

    public VinculosController(IVinculoService vinculoService)
    {
        _vinculoService = vinculoService;
    }

    [HttpGet]
    public async Task<ActionResult> ListarTodos()
    {
        // Retorna todos os vínculos
        var vinculos = await _vinculoService.ListarManifestosDeEscala(0); // Pode ajustar para um método que liste tudo
        return Ok(vinculos);
    }

    [HttpGet("manifesto/{manifestoId}")]
    public async Task<ActionResult<List<Escala>>> ListarPorManifesto(int manifestoId)
    {
        return Ok(await _vinculoService.ListarEscalasDeManifesto(manifestoId));
    }

    [HttpGet("escala/{escalaId}")]
    public async Task<ActionResult<List<Manifesto>>> ListarPorEscala(int escalaId)
    {
        return Ok(await _vinculoService.ListarManifestosDeEscala(escalaId));
    }

    [HttpPost("{manifestoId}/{escalaId}")]
    public async Task<ActionResult> CriarVinculo(int manifestoId, int escalaId)
    {
        var sucesso = await _vinculoService.Vincular(manifestoId, escalaId);

        if (!sucesso) return BadRequest("Não foi possível criar o vínculo.");
        return Ok();
    }

    [HttpDelete("{manifestoId}/{escalaId}")]
    public async Task<ActionResult> DeletarVinculo(int manifestoId, int escalaId)
    {
        var sucesso = await _vinculoService.Desvincular(manifestoId, escalaId);
        if (!sucesso) return NotFound();
        return NoContent();
    }
}
