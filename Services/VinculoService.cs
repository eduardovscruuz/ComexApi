using ComexApi.Data;
using ComexApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ComexApi.Services;

public interface IVinculoService
{
    public Task<List<Escala>> ListarEscalasDeManifesto(int manifestoId);
    public Task<List<Manifesto>> ListarManifestosDeEscala(int escalaId);
    public Task<bool> Vincular(int manifestoId, List<int> escalasIds);
    public Task<bool> Desvincular(int manifestoId, int escalaId);

}

public class VinculoService : IVinculoService
{
    private readonly AppDbContext _context;

    public VinculoService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<bool> Vincular(int manifestoId, List<int> escalasIds)
    {
        var manifesto = await _context.TabelaDeManifestos.FindAsync(manifestoId);
        if (manifesto == null)
            return false;

        foreach (var escalaId in escalasIds)
        {
            var escala = await _context.TabelaDeEscalas.FindAsync(escalaId);
            if (escala == null) continue;

            // Validações básicas
            if (_context.TabelaDeVinculos.Any(v => v.ManifestoId == manifestoId && v.EscalaId == escalaId))
                return false;

            if (escala.Status == StatusEscala.CANCELADA)
                return false;

            if (escala.Navio != manifesto.Navio)
                return false;

            // Criar vínculo
            var vinculo = new VinculoManifestoEscala
            {
                ManifestoId = manifestoId,
                EscalaId = escalaId,
                DataVinculacao = DateTime.Now
            };

            _context.TabelaDeVinculos.Add(vinculo);
        }

        await _context.SaveChangesAsync();
        return true;
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

    public async Task<bool> Desvincular(int manifestoId, int escalaId)
    {
        var vinculo = await _context.TabelaDeVinculos.FindAsync(manifestoId, escalaId);
        if (vinculo == null)
            return false;

        _context.TabelaDeVinculos.Remove(vinculo);
        await _context.SaveChangesAsync();
        return true;
    }

}

