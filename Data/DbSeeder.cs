using ComexApi.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ComexApi.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // Abre conexão e cria transação
        await context.Database.OpenConnectionAsync();
        using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            // 1️⃣ Escalas
            if (!context.TabelaDeEscalas.Any())
            {
                var escalasJson = File.ReadAllText("Data/Seed/escalas.json");
                var escalas = JsonConvert.DeserializeObject<List<Escala>>(escalasJson)!;

                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT TabelaDeEscalas ON");
                context.TabelaDeEscalas.AddRange(escalas);
                await context.SaveChangesAsync();
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT TabelaDeEscalas OFF");
            }

            // 2️⃣ Manifestos
            if (!context.TabelaDeManifestos.Any())
            {
                var manifestosJson = File.ReadAllText("Data/Seed/manifestos.json");
                var manifestos = JsonConvert.DeserializeObject<List<Manifesto>>(manifestosJson)!;

                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT TabelaDeManifestos ON");
                context.TabelaDeManifestos.AddRange(manifestos);
                await context.SaveChangesAsync();
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT TabelaDeManifestos OFF");
            }

            // 3️⃣ Vínculos
            if (!context.TabelaDeVinculos.Any() && File.Exists("Data/Seed/manifestos_escalas.json"))
            {
                var vinculosJson = File.ReadAllText("Data/Seed/manifestos_escalas.json");
                var vinculos = JsonConvert.DeserializeObject<List<VinculoManifestoEscala>>(vinculosJson)!;

                context.TabelaDeVinculos.AddRange(vinculos);
                await context.SaveChangesAsync();
            }

            // Commit da transação
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
        finally
        {
            await context.Database.CloseConnectionAsync();
        }
    }
}
