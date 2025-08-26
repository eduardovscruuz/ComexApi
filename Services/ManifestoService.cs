using ComexApi.Data;
using ComexApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ComexApi.Services;

public class ManifestoService : IManifestoService
{
    private readonly AppDbContext _context;

    public ManifestoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Manifesto>> GetAllManifestos()
    {
        return await _context.Manifestos.ToListAsync();
    }

    public async Task<Manifesto> GetManifestoById(int id)
    {
        return await _context.Manifestos.FirstAsync(m => m.Id == id);
    }


}
