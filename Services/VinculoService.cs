using ComexApi.Data;
using Microsoft.EntityFrameworkCore;

namespace ComexApi.Services;

public interface IVinculoService
{
    Task<bool> VincularManifestosEscalas(int escalaId, int manifestoId);
    Task<bool> DesvincularManifestosEscalas(int escalaId, int manifestoId);

}

public class VinculoService : IVinculoService
{
    private readonly AppDbContext _context;

    public VinculoService(AppDbContext context)
    {
        _context = context;
    }
    public async Task<bool> VincularManifestosEscalas(int escalaId, int manifestoId)
    {

        return await _context.ManifestosEscalas.Include(e => e.EscalaId).Include(m => m.ManifestoId).ToListAsync();


    }

    public async Task<bool> DesvincularManifestosEscalas(int escalaId, int manifestoId)
    {
        return await;
    }

}
