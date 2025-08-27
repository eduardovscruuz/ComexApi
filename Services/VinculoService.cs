using ComexApi.Data;
using ComexApi.Models;
using Microsoft.EntityFrameworkCore;


public interface IVinculoService
{
    Task<List<Vinculo>> ListarTodos();
    Task<List<Escala>> ListarEscalasDeManifesto(int manifestoId);
    Task<List<Manifesto>> ListarManifestosDeEscala(int escalaId);
    Task<bool> Vincular(int manifestoId, int escalaId);
    Task<bool> Desvincular(int manifestoId, int escalaId);
}

public class VinculoService : IVinculoService
{
    private readonly AppDbContext _context;
    public VinculoService(AppDbContext context) { _context = context; }

    public async Task<List<Vinculo>> ListarTodos()
    {
        return await _context.TabelaDeVinculos
            .Include(v => v.Escala)
            .Include(v => v.Manifesto)
            .ToListAsync();
    }

    public async Task<List<Escala>> ListarEscalasDeManifesto(int manifestoId)
    {
        return await _context.TabelaDeVinculos
            .Where(v => v.ManifestoId == manifestoId)
            .Select(v => v.Escala)
            .ToListAsync();
    }

    public async Task<List<Manifesto>> ListarManifestosDeEscala(int escalaId)
    {
        return await _context.TabelaDeVinculos
            .Where(v => v.EscalaId == escalaId)
            .Select(v => v.Manifesto)
            .ToListAsync();
    }

    public async Task<bool> Vincular(int manifestoId, int escalaId)
    {
        var manifesto = await _context.TabelaDeManifestos.FindAsync(manifestoId);
        var escala = await _context.TabelaDeEscalas.FindAsync(escalaId);

        if (manifesto == null || escala == null) return false;
        if (escala.Status == StatusEscala.CANCELADA) return false;
        if (escala.Navio != manifesto.Navio) return false;

        // Evitar duplicado
        if (await _context.TabelaDeVinculos.AnyAsync(v => v.ManifestoId == manifestoId && v.EscalaId == escalaId))
            return false;

        var vinculo = new Vinculo
        {
            ManifestoId = manifestoId,
            EscalaId = escalaId,
            DataVinculacao = DateTime.Now
        };

        _context.TabelaDeVinculos.Add(vinculo);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Desvincular(int manifestoId, int escalaId)
    {
        var vinculo = await _context.TabelaDeVinculos.FindAsync(manifestoId, escalaId);
        if (vinculo == null) return false;

        _context.TabelaDeVinculos.Remove(vinculo);
        await _context.SaveChangesAsync();
        return true;
    }
}
