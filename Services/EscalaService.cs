using ComexApi.Data;
using ComexApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ComexApi.Services;

public interface IEscalaService
{
    Task<List<Escala>> ListarEscalas();
    Task<Escala?> BuscarEscalaPorId(int id);
    Task<Escala> CriarEscala(Escala escala);
    Task<bool> DeletarEscala(int id);
}

public class EscalaService : IEscalaService
{
    private readonly AppDbContext _context;

    public EscalaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Escala> CriarEscala(Escala escala)
    {
        _context.TabelaDeEscalas.Add(escala);
        await _context.SaveChangesAsync();
        return escala;
    }
    public async Task<List<Escala>> ListarEscalas()
    {
        return await _context.TabelaDeEscalas.ToListAsync();
    }

    public async Task<Escala?> BuscarEscalaPorId(int id)
    {
        return await _context.TabelaDeEscalas.FindAsync(id);
    }

    public async Task<bool> DeletarEscala(int id)
    {
        var escalaExiste = await _context.TabelaDeEscalas.FindAsync(id);
        if (escalaExiste == null) return false;

        _context.TabelaDeEscalas.Remove(escalaExiste);
        await _context.SaveChangesAsync();
        return true;

    }

}
