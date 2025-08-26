using ComexApi.Data;
using ComexApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ComexApi.Services;

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

    public ManifestoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Manifesto>> ListarManifestos() => await _context.TabelaDeManifestos.ToListAsync();

    public async Task<Manifesto?> BuscarManifestoPorId(int id) => await _context.TabelaDeManifestos.FindAsync(id);

    public async Task<Manifesto> CriarManifesto(Manifesto manifesto)
    {
        _context.TabelaDeManifestos.Add(manifesto);
        await _context.SaveChangesAsync();
        return manifesto;
    }

    public async Task<bool> DeletarManifesto(int id)
    {
        var existente = await _context.TabelaDeManifestos.FindAsync(id);
        if (existente == null) return false;

        _context.TabelaDeManifestos.Remove(existente);
        await _context.SaveChangesAsync();
        return true;
    }

}
