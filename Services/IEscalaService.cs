using ComexApi.Models;

namespace ComexApi.Services;

public interface IEscalaService
{
    Task<List<Escala>> GetAllEscalas();
    Task<Escala> GetEscalaById(int id);
}
