using ComexApi.Models;

namespace ComexApi.Services;

public interface IManifestoService
{
    Task<List<Manifesto>> GetAllManifestos();
    Task<Manifesto> GetManifestoById(int id);
}
