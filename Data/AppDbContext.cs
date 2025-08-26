using ComexApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ComexApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Escala> Escalas { get; set; }
    public DbSet<Manifesto> Manifestos { get; set; }
    public DbSet<ManifestoEscala> ManifestosEscalas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Escala>().Property(e => e.Status).HasConversion<string>();
        modelBuilder.Entity<Manifesto>().Property(m => m.Tipo).HasConversion<string>();


    }
}
