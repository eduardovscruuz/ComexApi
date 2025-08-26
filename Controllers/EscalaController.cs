using ComexApi.Models;
using ComexApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ComexApi.Controllers;

[ApiController]
[Route("api/escalas")]
public class EscalaController : ControllerBase
{
    private readonly IEscalaService _service;
    public EscalaController(IEscalaService service) => _service = service;


    [HttpGet]
    public async Task<IActionResult> Listar() => Ok(await _service.ListarEscalas());

    [HttpGet("{id}")]
    public async Task<IActionResult> Buscar(int id) => Ok(await _service.BuscarEscalaPorId(id));

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] Escala escala)
    {
        var criada = await _service.CriarEscala(escala);
        return CreatedAtAction(nameof(Buscar), new { id = criada.Id }, criada);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        if (!await _service.DeletarEscala(id)) return NotFound();
        return NoContent();
    }
}