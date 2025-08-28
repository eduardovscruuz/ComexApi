using ComexApi.Data;
using ComexApi.Models;
using Microsoft.EntityFrameworkCore;


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
    public EscalaService(AppDbContext context) { _context = context; }

    public async Task<Escala> CriarEscala(Escala escala)
    {
        _context.TabelaDeEscalas.Add(escala);
        await _context.SaveChangesAsync();
        return escala;
    }

    public async Task<List<Escala>> ListarEscalas()
    {
        return await _context.TabelaDeEscalas
                 .Include(e => e.ManifestosVinculados)
                   .ThenInclude(v => v.Manifesto)
                 .ToListAsync();
    }

    public async Task<Escala?> BuscarEscalaPorId(int id)
    {
        return await _context.TabelaDeEscalas
            .Include(e => e.ManifestosVinculados)
                .ThenInclude(v => v.Manifesto)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<bool> DeletarEscala(int id)
    {
        var escala = await _context.TabelaDeEscalas.FindAsync(id);
        if (escala == null) return false;

        // Deletar todos os vínculos dessa escala
        var vinculos = _context.TabelaDeVinculos.Where(v => v.EscalaId == id);
        _context.TabelaDeVinculos.RemoveRange(vinculos);

        _context.TabelaDeEscalas.Remove(escala);
        await _context.SaveChangesAsync();
        return true;
    }
}
