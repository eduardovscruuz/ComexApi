using ComexApi.Data;

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

    public Task<bool> DesvincularManifestosEscalas(int escalaId, int manifestoId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> VincularManifestosEscalas(int escalaId, int manifestoId)
    {
        throw new NotImplementedException();
    }
}
