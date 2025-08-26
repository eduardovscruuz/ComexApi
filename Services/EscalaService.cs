using ComexApi.Data;
using ComexApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ComexApi.Services;

public interface IEscalaService
{
    Task<List<Escala>> GetAllEscalas();
    Task<Escala> GetEscalaById(int id);
}

public class EscalaService : IEscalaService
{
    private readonly AppDbContext _context;

    public EscalaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Escala>> GetAllEscalas()
    {
        return await _context.Escalas.ToListAsync();
    }


    public async Task<Escala> GetEscalaById(int id)
    {
        return await _context.Escalas.FirstAsync(e => e.Id == id);
    }
}
