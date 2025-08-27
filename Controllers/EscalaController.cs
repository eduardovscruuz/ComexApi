using ComexApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComexApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EscalasController : ControllerBase
{
    private readonly IEscalaService _escalaService;

    public EscalasController(IEscalaService escalaService)
    {
        _escalaService = escalaService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Escala>>> Listar()
    {
        return Ok(await _escalaService.ListarEscalas());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Escala>> BuscarPorId(int id)
    {
        var escala = await _escalaService.BuscarEscalaPorId(id);
        if (escala == null) return NotFound();
        return Ok(escala);
    }

    [HttpPost]
    public async Task<ActionResult<Escala>> Criar(Escala escala)
    {
        var criada = await _escalaService.CriarEscala(escala);
        return CreatedAtAction(nameof(BuscarPorId), new { id = criada.Id }, criada);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Deletar(int id)
    {
        var deletou = await _escalaService.DeletarEscala(id);
        if (!deletou) return NotFound();
        return NoContent();
    }
}
