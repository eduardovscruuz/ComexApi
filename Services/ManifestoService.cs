using ComexApi.Data;
using ComexApi.Models;
using Microsoft.EntityFrameworkCore;

public interface IManifestoService
{
    Task<List<Manifesto>> ListarManifestos();
    Task<Manifesto?> BuscarManifestoPorId(int id);
    Task<Manifesto> CriarManifesto(Manifesto manifesto);
    Task<bool> DeletarManifesto(int id);
}

public class ManifestoService : IManifestoService
{
    private readonly AppDbContext _context;
    public ManifestoService(AppDbContext context) { _context = context; }

    public async Task<Manifesto> CriarManifesto(Manifesto manifesto)
    {
        _context.TabelaDeManifestos.Add(manifesto);
        await _context.SaveChangesAsync();
        return manifesto;
    }

    public async Task<List<Manifesto>> ListarManifestos()
    {
        return await _context.TabelaDeManifestos
                  .Include(m => m.EscalasVinculadas)
                    .ThenInclude(v => v.Escala)
                  .ToListAsync();
    }

    public async Task<Manifesto?> BuscarManifestoPorId(int id)
    {
        return await _context.TabelaDeManifestos.FindAsync(id);
    }

    public async Task<bool> DeletarManifesto(int id)
    {
        var manifesto = await _context.TabelaDeManifestos.FindAsync(id);
        if (manifesto == null) return false;

        // Deletar todos os vínculos desse manifesto
        var vinculos = _context.TabelaDeVinculos.Where(v => v.ManifestoId == id);
        _context.TabelaDeVinculos.RemoveRange(vinculos);

        _context.TabelaDeManifestos.Remove(manifesto);
        await _context.SaveChangesAsync();
        return true;
    }
}
