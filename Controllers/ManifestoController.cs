using ComexApi.Models;
using ComexApi.Services;
using Microsoft.AspNetCore.Mvc;


namespace ComexApi.Controllers;

[ApiController]
[Route("api/manifestos")]
public class ManifestoController : ControllerBase
{
    private readonly IManifestoService _service;
    public ManifestoController(IManifestoService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> Listar() => Ok(await _service.ListarManifestos());

    [HttpGet("{id}")]
    public async Task<IActionResult> Buscar(int id) => Ok(await _service.BuscarManifestoPorId(id));

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] Manifesto manifesto)
    {
        var criada = await _service.CriarManifesto(manifesto);
        return CreatedAtAction(nameof(Buscar), new { id = criada.Id }, criada);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        if (!await _service.DeletarManifesto(id)) return NotFound();
        return NoContent();
    }
}