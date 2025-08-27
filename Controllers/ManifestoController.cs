using ComexApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComexApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ManifestosController : ControllerBase
{
    private readonly IManifestoService _manifestoService;

    public ManifestosController(IManifestoService manifestoService)
    {
        _manifestoService = manifestoService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Manifesto>>> Listar()
    {
        return Ok(await _manifestoService.ListarManifestos());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Manifesto>> BuscarPorId(int id)
    {
        var manifesto = await _manifestoService.BuscarManifestoPorId(id);
        if (manifesto == null) return NotFound();
        return Ok(manifesto);
    }

    [HttpPost]
    public async Task<ActionResult<Manifesto>> Criar(Manifesto manifesto)
    {
        var criado = await _manifestoService.CriarManifesto(manifesto);
        return CreatedAtAction(nameof(BuscarPorId), new { id = criado.Id }, criado);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Deletar(int id)
    {
        var deletou = await _manifestoService.DeletarManifesto(id);
        if (!deletou) return NotFound();
        return NoContent();
    }
}
