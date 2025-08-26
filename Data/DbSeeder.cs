using ComexApi.Models;
using Newtonsoft.Json;

namespace ComexApi.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // Manifestos
        if (!context.TabelaDeManifestos.Any())
        {
            var manifestosJson = File.ReadAllText("Data/Seed/manifestos.json");
            var manifestos = JsonConvert.DeserializeObject<List<Manifesto>>(manifestosJson);
            context.TabelaDeManifestos.AddRange(manifestos!);
        }

        // Escalas
        if (!context.TabelaDeEscalas.Any())
        {
            var escalasJson = File.ReadAllText("Data/Seed/escalas.json");
            var escalas = JsonConvert.DeserializeObject<List<Escala>>(escalasJson);
            context.TabelaDeEscalas.AddRange(escalas!);
        }

        // Vínculos (opcional)
        if (!context.TabelaDeVinculos.Any() && File.Exists("Data/Seed/manifestos_escalas.json"))
        {
            var vinculosJson = File.ReadAllText("Data/Seed/manifestos_escalas.json");
            var vinculos = JsonConvert.DeserializeObject<List<VinculoManifestoEscala>>(vinculosJson);
            context.TabelaDeVinculos.AddRange(vinculos!);
        }

        await context.SaveChangesAsync();
    }
}
