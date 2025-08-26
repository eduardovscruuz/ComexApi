using ComexApi.Data;
using ComexApi.Models;

namespace ComexApi.Services;

public interface IManifestoService
{
    Task<List<Manifesto>> GetAllManifestos();
    Task<Manifesto> GetManifestoById(int id);
}

public class ManifestoService : IManifestoService
{
    private readonly AppDbContext _context;

    public ManifestoService(AppDbContext context)
    {
        _context = context;
    }

    public Task<List<Manifesto>> GetAllManifestos()
    {
        throw new NotImplementedException();
    }

    public Task<Manifesto> GetManifestoById(int id)
    {
        throw new NotImplementedException();
    }
}
